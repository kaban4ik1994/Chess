using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using Chess.Core.Bot;
using Chess.Core.Models;
using Chess.Entities.Models;
using Chess.Enums;
using FastMember;
using Newtonsoft.Json;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using GameResult = ilf.pgn.Data.GameResult;

namespace Chess.DebutGameAnalyzer
{
    public static class Analyzer
    {
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private static string Hash(string chessBoardSerialize, string jsonPositionTo, string jsonPositionFrom,
            Color color)
        {
            using (var md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash,
                    $"{chessBoardSerialize}{jsonPositionFrom}{jsonPositionTo}{color.ToString("G")}");
            }
        }

        public static void AnalyzeMove(ExtendedPosition position, Color color, GameResult gameResult,
            IChessboard chessboard, IUnitOfWorkAsync unitOfWork, IRepositoryAsync<DebutGame> debutGaemeRepository,
            HashSet<DebutGame> cache)
        {
            if (gameResult == GameResult.Draw) return;

            var jsonPositionTo = JsonConvert.SerializeObject(position.To);
            var jsonPositionFrom = JsonConvert.SerializeObject(position.From);
            var chessBoardSerialize = chessboard.SerializedBoard();
            var gameHash = Hash(chessBoardSerialize, jsonPositionTo, jsonPositionFrom, color);

            var newDebut = new DebutGame
            {
                Id = gameHash,
                NextMove = jsonPositionTo,
                MoveColor = color,
                TotalGame = 1,
                Log = chessBoardSerialize,
                FromPosition = jsonPositionFrom,
                WinColor = gameResult == GameResult.Black ? Color.Black : Color.White
            };
            cache.Add(newDebut);

            //var entity = debutGaemeRepository.Find(gameHash);

            //if (entity != null)
            //{

            //    entity.TotalGame += 1;
            //    if (gameResult == GameResult.Black)
            //        entity.TotalBlackWinGames += 1;

            //    if (gameResult == GameResult.White)
            //        entity.TotalWhiteWinGames += 1;

            //    entity.BlackWinPercent = (entity.TotalBlackWinGames * 100.0 ) / entity.TotalGame;
            //    entity.WhiteWinPercent = (entity.TotalWhiteWinGames * 100.0) / entity.TotalGame;
            //    debutGaemeRepository.Update(entity);
            //}
            //else
            //{
            //    var newDebut = new DebutGame
            //    {
            //        NextMove = jsonPositionTo,
            //        MoveColor = color,
            //        TotalGame = 1,
            //        Log = chessBoardSerialize,
            //        FromPosition = jsonPositionFrom
            //    };

            //    if (gameResult == GameResult.Black)
            //        newDebut.TotalBlackWinGames += 1;

            //    if (gameResult == GameResult.White)
            //        newDebut.TotalWhiteWinGames += 1;
            //    newDebut.BlackWinPercent = (newDebut.TotalBlackWinGames * 100.0) / newDebut.TotalGame;
            //    newDebut.WhiteWinPercent = (newDebut.TotalWhiteWinGames * 100.0) / newDebut.TotalGame;

            //    debutGaemeRepository.Insert(newDebut);
            //}
            //unitOfWork.SaveChanges();
        }

        public static int InsertOrUpdateRecords(IEnumerable<DebutGame> records, string connectionString)
        {
            int result;
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("", conn))
                {
                    try
                    {
                        conn.Open();

                        //Creating temp table on database
                        command.CommandText =
                            "CREATE TABLE #TmpTable(" +
                            " [Id] [nvarchar](128) NOT NULL," +
                            " [Log] [nvarchar](max) NULL," +
                            " [NextMove] [nvarchar](max) NULL," +
                            " [FromPosition] [nvarchar](max) NULL," +
                            " [WhiteWinPercent] [float] NOT NULL," +
                            " [BlackWinPercent] [float] NOT NULL," +
                            " [TotalBlackWinGames] [bigint] NOT NULL," +
                            " [TotalWhiteWinGames] [bigint] NOT NULL," +
                            " [TotalGame] [bigint] NOT NULL," +
                            " [MoveColor] [int] NOT NULL," +
                            " [WinColor] [int] NOT NULL)";
                        command.ExecuteNonQuery();

                        //Bulk insert into temp table
                        using (var sbc = new SqlBulkCopy(conn))
                        using (
                            var objectReader = ObjectReader.Create(records, "Id", "Log",
                                "NextMove", "FromPosition", "WhiteWinPercent", "BlackWinPercent", "TotalBlackWinGames", "TotalWhiteWinGames", "TotalGame", "MoveColor", "WinColor"))
                        {
                            sbc.DestinationTableName = "#TmpTable";
                            sbc.BatchSize = 1000;
                            sbc.ColumnMappings.Add("Id", "Id");
                            sbc.ColumnMappings.Add("Log", "Log");
                            sbc.ColumnMappings.Add("NextMove", "NextMove");
                            sbc.ColumnMappings.Add("FromPosition", "FromPosition");
                            sbc.ColumnMappings.Add("WhiteWinPercent", "WhiteWinPercent");
                            sbc.ColumnMappings.Add("BlackWinPercent", "BlackWinPercent");
                            sbc.ColumnMappings.Add("TotalBlackWinGames", "TotalBlackWinGames");
                            sbc.ColumnMappings.Add("TotalWhiteWinGames", "TotalWhiteWinGames");
                            sbc.ColumnMappings.Add("TotalGame", "TotalGame");
                            sbc.ColumnMappings.Add("MoveColor", "MoveColor");
                            sbc.ColumnMappings.Add("WinColor", "WinColor");
                            sbc.WriteToServer(objectReader);
                            sbc.Close();
                        }

                        // Updating destination table, and dropping temp table
                        command.CommandTimeout = 60 * 60; //60 minutes; 
                        command.CommandText = "MERGE INTO [dbo].[DebutGames] AS Target " +
                                              "USING #TmpTable AS Source " +
                                              "ON Target.[Id] = Source.[Id] " +
                                              "WHEN MATCHED THEN " +
                                              "UPDATE " +
                                              "SET Target.[TotalGame] = Target.[TotalGame] + Source.[TotalGame], " +
                                              "Target.[TotalBlackWinGames] = Target.[TotalBlackWinGames] + Source.[TotalBlackWinGames], " +
                                              "Target.[TotalWhiteWinGames] = Target.[TotalWhiteWinGames] + Source.[TotalWhiteWinGames], " +
                                              "Target.[BlackWinPercent] = ( (Target.[TotalBlackWinGames] + Source.[TotalBlackWinGames]) * 100.0 ) / (Target.[TotalGame] + Source.[TotalGame]), " +
                                              "Target.[WhiteWinPercent] = ( (Target.[TotalWhiteWinGames] + Source.[TotalWhiteWinGames]) * 100.0 ) / (Target.[TotalGame] + Source.[TotalGame]) " +
                                              "WHEN NOT MATCHED BY TARGET THEN " +
                                              "INSERT (Id, Log, NextMove, FromPosition, WhiteWinPercent, BlackWinPercent, TotalBlackWinGames, TotalWhiteWinGames, TotalGame, MoveColor, WinColor) " +
                                              "VALUES (Source.[Id], Source.[Log], Source.[NextMove], Source.[FromPosition], Source.[WhiteWinPercent], Source.[BlackWinPercent], Source.[TotalBlackWinGames], Source.[TotalWhiteWinGames], Source.[TotalGame], Source.[MoveColor], Source.[WinColor]); " +
                                              "DROP TABLE #TmpTable; ";
                        result = command.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }
    }
}
