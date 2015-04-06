using System;
using System.Linq;
using Chess.Core.Enums;
using Chess.Core.FactoryFigures;
using Newtonsoft.Json;

namespace Chess.Core.Models
{
    public class Chessboard : IChessboard
    {
        public Cell[,] Board { get; set; }
        private string _oldBoard = string.Empty;

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
                for (var y = 7; y >= 0; y--)
                {
                    Board[x, y] = new Cell
                    {
                        Position = new Position { X = ConvertIntToPositionX(y), Y = ConvertIntToPositionY(Math.Abs(x - 7)) },
                    };
                }
            }
        }

        public void UndoLastMove()
        {
            if (!string.IsNullOrEmpty(_oldBoard))
            {
                DeserializeBoard(_oldBoard);
            }
        }

        public void SetFigureByPosition(Figure figure, Position position)
        {
            _oldBoard = SerializedBoard();
            GetCellByPosition(position).Figure = figure;
        }

        public void ChangeThePositionOfTheFigure(Position from, Position to)
        {
            _oldBoard = SerializedBoard();
            var cellFrom = GetCellByPosition(from);
            var cellTo = GetCellByPosition(to);
            cellFrom.Figure.IsMakeFirstMove = true;
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
            var cell = Board.Cast<Cell>().First(x => x.Position.Equals(position));
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

        public char IncrementX(char x)
        {
            var intX = ConvertPositionXToInt(x);
            intX++;
            return ConvertIntToPositionX(intX);
        }

        public char DecrementX(char x)
        {
            var intX = ConvertPositionXToInt(x);
            intX--;
            return ConvertIntToPositionX(intX);
        }

        public int IncrementY(int y)
        {
            var intY = ConvertPositionYToInt(y);
            intY++;
            return ConvertIntToPositionY(intY);
        }

        public int DecrementY(int y)
        {
            var intY = ConvertPositionYToInt(y);
            intY--;
            return ConvertIntToPositionY(intY);
        }

        public string SerializedBoard()
        {
            return JsonConvert.SerializeObject(Board);
        }

        public void DeserializeBoard(string value)
        {
            Board = JsonConvert.DeserializeObject<Cell[,]>(value);
        }

        public char ConvertIntToPositionX(int x)
        {
            if (x > 7 || x < 0) return ' ';
            return Convert.ToChar(x + 65);
        }

        public int ConvertPositionYToInt(int y)
        {
            return y - 1;
        }

        public int ConvertIntToPositionY(int y)
        {
            if (y > 7 || y < 0) return -1;
            return y + 1;
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

        public bool IsValidPositionAndEmptyCell(Position position)
        {
            return position.X != ' ' && position.Y != -1 && GetFigureByPosition(position) == null;
        }

        public bool IsValidPosition(Position position)
        {
            return position.X != ' ' && position.Y != -1;
        }
    }
}
