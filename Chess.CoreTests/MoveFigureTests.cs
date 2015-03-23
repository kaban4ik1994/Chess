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


            Assert.IsTrue(mediator.Send(new Position { X = 'H', Y = 7 }, new Position { X = 'H', Y = 5 }, board));
            Assert.IsFalse(mediator.Send(new Position { X = 'H', Y = 5 }, new Position { X = 'H', Y = 3 }, board));
            Assert.IsTrue(mediator.Send(new Position { X = 'H', Y = 5 }, new Position { X = 'H', Y = 4 }, board));
            Assert.IsTrue(mediator.Send(new Position { X = 'H', Y = 4 }, new Position { X = 'H', Y = 3 }, board));
            Assert.IsFalse(mediator.Send(new Position { X = 'H', Y = 3 }, new Position { X = 'H', Y = 2 }, board));
            Assert.IsTrue(mediator.Send(new Position { X = 'G', Y = 2 }, new Position { X = 'H', Y = 3 }, board));

            Assert.IsTrue(mediator.Send(new Position { X = 'A', Y = 2 }, new Position { X = 'A', Y = 3 }, board));
            Assert.IsTrue(mediator.Send(new Position { X = 'B', Y = 2 }, new Position { X = 'B', Y = 4 }, board));
            Assert.IsFalse(mediator.Send(new Position { X = 'A', Y = 2 }, new Position { X = 'B', Y = 2 }, board));
            Assert.IsFalse(mediator.Send(new Position { X = 'A', Y = 3 }, new Position { X = 'A', Y = 2 }, board));

            Assert.IsTrue(mediator.Send(new Position { X = 'A', Y = 7 }, new Position { X = 'A', Y = 6 }, board));
            Assert.IsTrue(mediator.Send(new Position { X = 'B', Y = 7 }, new Position { X = 'B', Y = 5 }, board));
            Assert.IsFalse(mediator.Send(new Position { X = 'A', Y = 7 }, new Position { X = 'B', Y = 7 }, board));
            Assert.IsFalse(mediator.Send(new Position { X = 'A', Y = 6 }, new Position { X = 'A', Y = 7 }, board));
        }
    }
}
