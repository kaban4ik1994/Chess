using System;
using System.Linq;
using Chess.Core.Enums;
using Chess.Core.FactoryFigures;

namespace Chess.Core.Models
{
    public class Chessboard : IChessboard
    {
        public Cell[,] Board { get; set; }

        public ICreatorBishop CreatorBishop { get; private set; }
        public ICreatorKing CreatorKing { get; private set; }
        public ICreatorKnight CreatorKnight { get; private set; }
        public ICreatorPawn CreatorPawn { get; private set; }
        public ICreatorQueen CreatorQueen { get; private set; }
        public ICreatorRook CreatorRook { get; private set; }

        public Chessboard(ICreatorBishop creatorBishop, ICreatorKing creatorKing, ICreatorKnight creatorKnight, ICreatorPawn creatorPawn, ICreatorQueen creatorQueen, ICreatorRook creatorRook)
        {
            CreatorBishop = creatorBishop;
            CreatorKing = creatorKing;
            CreatorKnight = creatorKnight;
            CreatorPawn = creatorPawn;
            CreatorQueen = creatorQueen;
            CreatorRook = creatorRook;

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

        public void SetFigureByPosition(Figure figure, Position position)
        {
            GetCellByPosition(position).Figure = figure;
        }


        public void ChangeThePositionOfTheFigure(Position from, Position to)
        {
            var cellFrom = GetCellByPosition(from);
            var cellTo = GetCellByPosition(to);
            cellTo.Figure = new Figure(cellFrom.Figure);
            cellFrom.Figure = null;
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

        public Cell GetCellByPosition(Position position)
        {
            var cell = Board[ConvertPositionXToInt(position.X), ConvertPositionYToInt(position.Y)];
            return cell;
        }

        public Figure GetFigureByPosition(Position position)
        {
            var cell = Board.Cast<Cell>().First(x => x.Position.Equals(position));
            return cell.Figure;
        }

        public int ConvertPositionXToInt(char x)
        {
            return Convert.ToInt32(x - 65);
        }

        public int ConvertPositionYToInt(int y)
        {
            return y - 1;
        }

        private Figure GetFigureByStartingPosition(Position position)
        {
            if (position.Y == 2) return CreatorPawn.FactoryMethod(Color.White);
            if (position.Y == 7) return CreatorPawn.FactoryMethod(Color.Black);

            var color = Color.White;

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

            if (isEmptyCell) return null;

            switch (position.X)
            {
                case 'A':
                    return CreatorRook.FactoryMethod(color);
                case 'B':
                    return CreatorKnight.FactoryMethod(color);
                case 'C':
                    return CreatorBishop.FactoryMethod(color);
                case 'D':
                    return CreatorQueen.FactoryMethod(color);
                case 'E':
                    return CreatorKing.FactoryMethod(color);
                case 'F':
                    return CreatorBishop.FactoryMethod(color);
                case 'G':
                    return CreatorKnight.FactoryMethod(color);
                case 'H':
                    return CreatorRook.FactoryMethod(color);
            }
            return null;
        }
    }
}
