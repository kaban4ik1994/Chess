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
            var board = new Chessboard();

            var firstPosition = new Position { X = 'A', Y = 1 };
            var lastPosition = new Position { X = 'H', Y = 8 };

            Assert.IsTrue(board.Board[0, 0].Position.Equals(firstPosition));
            Assert.IsTrue(board.Board[7, 7].Position.Equals(lastPosition));
        }

        [TestMethod]
        public void Check_Parting_Figures()
        {
            var board = new Chessboard();
            board.InitNewGame();

            var creatorBishop = new CreatorBishop();
            var creatorKing = new CreatorKing();
            var creatorKnight = new CreatorKnight();
            var creatorPawn = new CreatorPawn();
            var creatorQueen = new CreatorQueen();
            var creatorRook = new CreatorRook();

            Assert.IsTrue(creatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'A', Y = 2 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'B', Y = 2 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'C', Y = 2 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'D', Y = 2 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'E', Y = 2 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'F', Y = 2 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'G', Y = 2 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'H', Y = 2 })));

            Assert.IsTrue(creatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'A', Y = 7 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'B', Y = 7 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'C', Y = 7 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'D', Y = 7 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'E', Y = 7 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'F', Y = 7 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'G', Y = 7 })));
            Assert.IsTrue(creatorPawn.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'H', Y = 7 })));

            Assert.IsTrue(creatorRook.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'A', Y = 1 })));
            Assert.IsTrue(creatorRook.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'A', Y = 8 })));

            Assert.IsTrue(creatorKnight.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'B', Y = 1 })));
            Assert.IsTrue(creatorKnight.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'B', Y = 8 })));

            Assert.IsTrue(creatorBishop.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'C', Y = 1 })));
            Assert.IsTrue(creatorBishop.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'C', Y = 8 })));

            Assert.IsTrue(creatorQueen.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'D', Y = 1 })));
            Assert.IsTrue(creatorQueen.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'D', Y = 8 })));

            Assert.IsTrue(creatorKing.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'E', Y = 1 })));
            Assert.IsTrue(creatorKing.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'E', Y = 8 })));

            Assert.IsTrue(creatorBishop.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'F', Y = 1 })));
            Assert.IsTrue(creatorBishop.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'F', Y = 8 })));

            Assert.IsTrue(creatorKnight.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'G', Y = 1 })));
            Assert.IsTrue(creatorKnight.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'G', Y = 8 })));

            Assert.IsTrue(creatorRook.FactoryMethod(Color.White).Equals(board.GetFigureByPosition(new Position { X = 'H', Y = 1 })));
            Assert.IsTrue(creatorRook.FactoryMethod(Color.Black).Equals(board.GetFigureByPosition(new Position { X = 'H', Y = 8 })));

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
