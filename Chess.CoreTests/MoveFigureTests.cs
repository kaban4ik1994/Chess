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

            var pawnColleague=new PawnColleague(meditor);

            meditor.PawnColleague = pawnColleague;

            Assert.IsFalse(pawnColleague.Send(new Position(), new Position(), board));
        }
    }
}
