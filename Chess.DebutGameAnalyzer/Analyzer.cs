using System.Linq;
using Chess.Core.Bot;
using Chess.Core.Models;
using Chess.Entities.Models;
using Chess.Enums;
using Newtonsoft.Json;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using GameResult = ilf.pgn.Data.GameResult;

namespace Chess.DebutGameAnalyzer
{
    public static class Analyzer
    {
        public static void AnalyzeMove(ExtendedPosition position, Color color, GameResult gameResult, IChessboard chessboard, IUnitOfWorkAsync unitOfWork, IRepositoryAsync<DebutGame> debutGaemeRepository)
        {
            if (gameResult == GameResult.Draw) return;

            var jsonPosition = JsonConvert.SerializeObject(position.To);
            var chessBoardSerialize = chessboard.SerializedBoard();
            var entity =
                debutGaemeRepository.Queryable()
                    .FirstOrDefault(
                        game =>
                            game.Log == chessBoardSerialize && game.NextMove == jsonPosition && game.MoveColor == color);
            if (entity != null)
            {

                entity.TotalGame += 1;
                if (gameResult == GameResult.Black)
                    entity.TotalBlackWinGames += 1;

                if (gameResult == GameResult.White)
                    entity.TotalWhiteWinGames += 1;

                entity.BlackWinPercent = (entity.TotalBlackWinGames * 100.0 ) / entity.TotalGame;
                entity.WhiteWinPercent = (entity.TotalWhiteWinGames * 100.0) / entity.TotalGame;
                debutGaemeRepository.Update(entity);
            }
            else
            {
                var newDebut = new DebutGame
                {
                    NextMove = jsonPosition,
                    MoveColor = color,
                    TotalGame = 1,
                    Log = chessBoardSerialize
                };

                if (gameResult == GameResult.Black)
                    newDebut.TotalBlackWinGames += 1;

                if (gameResult == GameResult.White)
                    newDebut.TotalWhiteWinGames += 1;
                newDebut.BlackWinPercent = (newDebut.TotalBlackWinGames * 100.0) / newDebut.TotalGame;
                newDebut.WhiteWinPercent = (newDebut.TotalWhiteWinGames * 100.0) / newDebut.TotalGame;

                debutGaemeRepository.Insert(newDebut);
            }
            unitOfWork.SaveChanges();
        }
    }
}
