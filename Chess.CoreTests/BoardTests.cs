using System;
using System.Runtime.InteropServices;
using Chess.Core.Enums;
using Chess.Core.FactoryFigures;
using Chess.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.CoreTests
{
    [TestClass]
    public class BoardTests
    {

        [TestMethod]
        public void Correct_creation_the_board()
        {
            var board = new Chessboard(new CreatorBishop(), new CreatorKing(), new CreatorKnight(), new CreatorPawn(), new CreatorQueen(), new CreatorRook());

            var firstPosition = new Position { X = 'A', Y = 1 };
            var lastPosition = new Position { X = 'H', Y = 8 };

            Assert.IsTrue(board.Board[0, 0].Position.Equals(firstPosition));
            Assert.IsTrue(board.Board[7, 7].Position.Equals(lastPosition));
        }

        [TestMethod]
        public void Check_Parting_Figures()
        {
            var board = new Chessboard(new CreatorBishop(), new CreatorKing(), new CreatorKnight(), new CreatorPawn(), new CreatorQueen(), new CreatorRook());

            board.InitNewGame();

            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'A', Y = 2 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'B', Y = 2 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'C', Y = 2 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'D', Y = 2 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'E', Y = 2 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'F', Y = 2 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'G', Y = 2 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'H', Y = 2 })));

            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'A', Y = 7 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'B', Y = 7 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'C', Y = 7 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'D', Y = 7 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'E', Y = 7 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'F', Y = 7 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'G', Y = 7 })));
            Assert.IsTrue(board.CreatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'H', Y = 7 })));

            Assert.IsTrue(board.CreatorRook.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'A', Y = 1 })));
            Assert.IsTrue(board.CreatorRook.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'A', Y = 8 })));

            Assert.IsTrue(board.CreatorKnight.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'B', Y = 1 })));
            Assert.IsTrue(board.CreatorKnight.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'B', Y = 8 })));

            Assert.IsTrue(board.CreatorBishop.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'C', Y = 1 })));
            Assert.IsTrue(board.CreatorBishop.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'C', Y = 8 })));

            Assert.IsTrue(board.CreatorQueen.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'D', Y = 1 })));
            Assert.IsTrue(board.CreatorQueen.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'D', Y = 8 })));

            Assert.IsTrue(board.CreatorKing.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'E', Y = 1 })));
            Assert.IsTrue(board.CreatorKing.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'E', Y = 8 })));

            Assert.IsTrue(board.CreatorBishop.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'F', Y = 1 })));
            Assert.IsTrue(board.CreatorBishop.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'F', Y = 8 })));

            Assert.IsTrue(board.CreatorKnight.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'G', Y = 1 })));
            Assert.IsTrue(board.CreatorKnight.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'G', Y = 8 })));

            Assert.IsTrue(board.CreatorRook.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'H', Y = 1 })));
            Assert.IsTrue(board.CreatorRook.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'H', Y = 8 })));

            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'A', Y = 3 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'A', Y = 4 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'A', Y = 5 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'A', Y = 6 }));

            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'B', Y = 3 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'B', Y = 4 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'B', Y = 5 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'B', Y = 6 }));

            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'C', Y = 3 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'C', Y = 4 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'C', Y = 5 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'C', Y = 6 }));

            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'D', Y = 3 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'D', Y = 4 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'D', Y = 5 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'D', Y = 6 }));

            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'E', Y = 3 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'E', Y = 4 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'E', Y = 5 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'E', Y = 6 }));

            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'F', Y = 3 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'E', Y = 4 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'E', Y = 5 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'E', Y = 6 }));

            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'G', Y = 3 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'G', Y = 4 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'G', Y = 5 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'G', Y = 6 }));

            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'H', Y = 3 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'H', Y = 4 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'H', Y = 5 }));
            Assert.IsNull(board.GetFigureByPosition(new Position { X = 'H', Y = 6 }));
        }
    }
}
