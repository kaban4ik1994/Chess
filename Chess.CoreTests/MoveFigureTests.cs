using System.Linq;
using Chess.Core.Enums;
using Chess.Core.FactoryFigures;
using Chess.Core.Mediator;
using Chess.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.CoreTests
{
    [TestClass]
    public class MoveFigureTests
    {
        [TestMethod]
        public void Pawn()
        {
            var board = new Chessboard(new CreatorBishop(), new CreatorKing(), new CreatorKnight(), new CreatorPawn(), new CreatorQueen(), new CreatorRook());
            board.InitNewGame();

            var mediator = new MoveMediator(new PawnColleague(), new QueenColleague(), new RookColleague(),
                new KingColleague(), new KnightColleague(), new BishopColleague());

            var pawnColleague = new PawnColleague();

            var moves = pawnColleague.GetPossibleMoves(new Position { X = 'B', Y = 2 }, board);

            Assert.IsTrue(moves.Any(x => x.Equals(new Position { X = 'B', Y = 3 })));
            Assert.IsTrue(moves.Any(x => x.Equals(new Position { X = 'B', Y = 4 })));

            moves = pawnColleague.GetPossibleMoves(new Position { X = 'B', Y = 7 }, board);

            Assert.IsTrue(moves.Any(x => x.Equals(new Position { X = 'B', Y = 6 })));
            Assert.IsTrue(moves.Any(x => x.Equals(new Position { X = 'B', Y = 5 })));

            mediator.Send(new Position { X = 'B', Y = 7 }, new Position { X = 'B', Y = 6 }, board);

            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'B', Y = 7 }));
            Assert.IsNotNull(board.GetFigureByPosition(new Position { X = 'B', Y = 6 }));

            board.SetFigureByPosition(board.CreatorPawn.FactoryMethod(Color.Black), new Position { X = 'B', Y = 3 });

            moves = pawnColleague.GetPossibleMoves(new Position { X = 'B', Y = 3 }, board);
            Assert.IsTrue(moves.Count() == 2);
        }
    }
}
