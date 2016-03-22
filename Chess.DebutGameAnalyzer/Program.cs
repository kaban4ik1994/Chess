using System;
using System.Linq;
using Chess.Core.Bot;
using Chess.Core.DebutDb;
using Chess.Core.Mediator;
using Chess.Core.Models;
using Chess.DebutGameAnalyzer.Helpers;
using Chess.Entities.Models;
using ilf.pgn.Data;
using Microsoft.Practices.Unity;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Color = Chess.Enums.Color;

namespace Chess.DebutGameAnalyzer
{
    internal class Program
    {
        private static void Main()
        {
            var debutDbLoader = new DebutDbLoader();
            var diContainer = IocHelper.Init();
            var debutGameRepository = diContainer.Resolve<IRepositoryAsync<DebutGame>>();
            var unitOfWorkAsync = diContainer.Resolve<IUnitOfWorkAsync>();
            var chessBoard = diContainer.Resolve<IChessboard>();

            var moveMediator = diContainer.Resolve<IMoveMediator>();
            var castlingService = diContainer.Resolve<ICastling>();
            for (var k = 0; k < debutDbLoader.CountOfFiles() - 1; k++)
            {
                var games = debutDbLoader.Load(k, 1);

                for (var i = 0; i < games.Count; i++)
                {
                    chessBoard.InitNewGame();
                    var moves = games[i].MoveText.GetMoves().ToList();
                    for (var j = 0; j < moves.Count(); j++)
                    {
                        var color = (j + 1)%2 != 0 ? Color.White : Color.Black;
                        var move = moves[j];
                        ExtendedPosition extendedPosition;
                        if (move.Type == MoveType.CastleKingSide)
                        {
                            extendedPosition = castlingService.GetShortCastlingMove(color);
                            Analyzer.AnalyzeMove(extendedPosition, color, games[i].Result, chessBoard, unitOfWorkAsync,
                                debutGameRepository);
                            moveMediator.Send(extendedPosition.From, extendedPosition.To, chessBoard, color);
                            continue;
                        }
                        if (move.Type == MoveType.CastleQueenSide)
                        {
                            extendedPosition = castlingService.GetLongCastlingMove(color);
                            Analyzer.AnalyzeMove(extendedPosition, color, games[i].Result, chessBoard, unitOfWorkAsync,
                                debutGameRepository);
                            moveMediator.Send(extendedPosition.From, extendedPosition.To, chessBoard, color);
                            continue;
                        }

                        if (move.Type == MoveType.CaptureEnPassant && !move.Piece.HasValue) break;

                        var to = new Position
                        {
                            X = Convert.ToChar(move.TargetSquare.File.ToString("G")),
                            Y = move.TargetSquare.Rank
                        };

                        extendedPosition =
                            moveMediator.GetAllMoves(color, chessBoard)
                                .FirstOrDefault(
                                    position =>
                                        position.To.Equals(to) &&
                                        chessBoard.GetFigureByPosition(position.From).Type.ToString("G") ==
                                        move.Piece.Value.ToString("G"));
                        if (extendedPosition == null) break;
                        Analyzer.AnalyzeMove(extendedPosition, color, games[i].Result, chessBoard, unitOfWorkAsync,
                            debutGameRepository);
                        moveMediator.Send(extendedPosition.From, extendedPosition.To, chessBoard, color);

                    }
                }
            }
        }
    }
}
