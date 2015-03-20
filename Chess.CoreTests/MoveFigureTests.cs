using System;
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
            var board = new Chessboard();
            board.InitNewGame();

            var meditor = new MoveMediator();

            var pawnColleague = new PawnColleague(meditor);

            meditor.PawnColleague = pawnColleague;

            Assert.IsTrue(pawnColleague.Send(new Position { X = 'A', Y = 2 }, new Position { X = 'A', Y = 3 }, board));
            Assert.IsFalse(pawnColleague.Send(new Position { X = 'A', Y = 2 }, new Position { X = 'B', Y = 2 }, board));
        }
    }
}
