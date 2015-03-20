using System;
using System.Linq;
using Chess.Core.Enums;
using Chess.Core.FactoryFigures;

namespace Chess.Core.Models
{
    public class Chessboard
    {
        public Cell[,] Board { get; set; }

        private readonly Creator _creatorBishop = new CreatorBishop();
        private readonly Creator _creatorKing = new CreatorKing();
        private readonly Creator _creatorKnight = new CreatorKnight();
        private readonly Creator _creatorPawn = new CreatorPawn();
        private readonly Creator _creatorQueen = new CreatorQueen();
        private readonly Creator _creatorRook = new CreatorRook();

        public Chessboard()
        {
            Board = new Cell[8, 8];

            for (var x = 0; x <= 7; x++)
            {
                for (var y = 0; y <= 7; y++)
                {
                    Board[x, y] = new Cell
                    {
                        Position = new Position { X = Convert.ToChar(x + 65), Y = y + 1 },
                    };
                }
            }
        }

        public Chessboard(string board)
        {

        }

        public void InitNewGame()
        {
            for (var x = 0; x <= 7; x++)
            {
                for (var y = 0; y <= 7; y++)
                {
                    Board[x, y].Figure = GetFigureByStartingPosition(Board[x, y].Position);
                }
            }
        }

        public Figure GetFigureByPosition(Position position)
        {
            var cell = Board.Cast<Cell>().FirstOrDefault(x => x.Position.Equals(position));
            return cell == null ? null : cell.Figure;
        }

        private Figure GetFigureByStartingPosition(Position position)
        {
            if (position.Y == 2) return _creatorPawn.FactoryMethod(Color.White);
            if (position.Y == 7) return _creatorPawn.FactoryMethod(Color.Black);

            var color=Color.White;

            var isEmptyCell = true;

            if (position.Y == 1)
            {
                color = Color.White;
                isEmptyCell = false;
            }

            if (position.Y == 8)
            {
                color = Color.Black;
                isEmptyCell = false;
            }
            if(isEmptyCell) return null;
            
            switch (position.X)
            {
                case 'A':
                    return _creatorRook.FactoryMethod(color);
                case 'B':
                    return _creatorKnight.FactoryMethod(color);
                case 'C':
                    return _creatorBishop.FactoryMethod(color);
                case 'D':
                    return _creatorQueen.FactoryMethod(color);
                case 'E':
                    return _creatorKing.FactoryMethod(color);
                case 'F':
                    return _creatorBishop.FactoryMethod(color);
                case 'G':
                    return _creatorKnight.FactoryMethod(color);
                case 'H':
                    return _creatorRook.FactoryMethod(color);
            }
            return null;
        }
    }
}
