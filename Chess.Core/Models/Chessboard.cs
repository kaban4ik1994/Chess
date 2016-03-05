using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Core.FactoryFigures;
using Chess.Enums;
using Newtonsoft.Json;

namespace Chess.Core.Models
{
	public class Chessboard : IChessboard
	{
		public Cell[,] Board { get; set; }

		private Stack<string> _oldBoards = new Stack<string>();

		private IDictionary<FigureType, int> _costFigures;

		private readonly int[,] _blackPawnPieceSquare;
		private readonly int[,] _whitePawnPieceSquare;
		private readonly int[,] _blackKnightPieceSquare;
		private readonly int[,] _whiteKnightPieceSquare;
		private readonly int[,] _blackBishopPieceSquare;
		private readonly int[,] _whiteBishopPieceSquare;
		private readonly int[,] _blackRookPieceSquare;
		private readonly int[,] _whiteRookPieceSquare;
		private readonly int[,] _blackQueenPieceSquare;
		private readonly int[,] _whiteQueenPieceSquare;
		private readonly int[,] _blackKingMiddleGamePieceSquare;
		private readonly int[,] _whiteKingMiddlePieceSquare;
		private readonly int[,] _blackKingEndGamePieceSquare;
		private readonly int[,] _whiteKingEndGamePieceSquare;

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
			_costFigures = new Dictionary<FigureType, int>()
			{
				{FigureType.Pawn, 100},
				{FigureType.Knight, 320},
				{FigureType.Bishop, 330},
				{FigureType.Rook, 500},
				{FigureType.Queen, 900},
				{FigureType.King, 20000}
			};

			Board = new Cell[8, 8];

			_blackPawnPieceSquare = new[,]
			{
				{0,  0,  0,  0,  0,  0,  0,  0},
				{50, 50, 50, 50, 50, 50, 50, 50},
				{10, 10, 20, 30, 30, 20, 10, 10},
				{5,  5, 10, 25, 25, 10,  5,  5},
				{0,  0,  0, 20, 20,  0,  0,  0},
				{5, -5,-10,  0,  0,-10, -5,  5},
				{5, 10, 10,-20,-20, 10, 10,  5},
				{0,  0,  0,  0,  0,  0,  0,  0},	
			};

			_whitePawnPieceSquare = new[,]
			{
				{0,  0,  0,  0,  0,  0,  0,  0},
				{5, 10, 10, -20, -20, 10, 10, 5},
				{5, -5, -10, 0, 0, -10, -5, 5},
				{0, 0, 0, 20, 20, 0, 0, 0},
				{5, 5, 10, 25, 25, 10, 5, 5},
				{10, 10, 20, 30, 30, 20, 10, 10},
				{50, 50, 50, 50, 50, 50, 50, 50},
				{0,  0,  0,  0,  0,  0,  0,  0}
			};

			_blackKnightPieceSquare = new[,]
			{
				{-50, -40, -30, -30, -30, -30, -40, -50},
				{-40, -20, 0, 0, 0, 0, -20, -40},
				{-30, 0, 10, 15, 15, 10, 0, -30},
				{-30, 5, 15, 20, 20, 15, 5, -30},
				{-30, 0, 15, 20, 20, 15, 0, -30},
				{-30, 5, 10, 15, 15, 10, 5, -30},
				{-40, -20, 0, 5, 5, 0, -20, -40},
				{-50, -40, -30, -30, -30, -30, -40, -50},
			};

			_whiteKnightPieceSquare = new[,]
			{
				{-50, -40, -30, -30, -30, -30, -40, -50},
				{-40, -20, 0, 5, 5, 0, -20, -40},
				{-30, 5, 10, 15, 15, 10, 5, -30},
				{-30, 0, 15, 20, 20, 15, 0, -30},
				{-30, 5, 15, 20, 20, 15, 5, -30},
				{-30, 0, 10, 15, 15, 10, 0, -30},
				{-40, -20, 0, 0, 0, 0, -20, -40},
				{-50, -40, -30, -30, -30, -30, -40, -50}
			};

			_blackBishopPieceSquare = new[,]
			{
				{-20, -10, -10, -10, -10, -10, -10, -20},
				{-10, 0, 0, 0, 0, 0, 0, -10},
				{-10, 0, 5, 10, 10, 5, 0, -10},
				{-10, 5, 5, 10, 10, 5, 5, -10},
				{-10, 0, 10, 10, 10, 10, 0, -10},
				{-10, 10, 10, 10, 10, 10, 10, -10},
				{-10, 5, 0, 0, 0, 0, 5, -10},
				{-20, -10, -10, -10, -10, -10, -10, -20}
			};

			_whiteBishopPieceSquare = new[,]
			{
				{-20, -10, -10, -10, -10, -10, -10, -20},
				{-10, 5, 0, 0, 0, 0, 5, -10},
				{-10, 10, 10, 10, 10, 10, 10, -10},
				{-10, 0, 10, 10, 10, 10, 0, -10},
				{-10, 5, 5, 10, 10, 5, 5, -10},
				{-10, 0, 5, 10, 10, 5, 0, -10},
				{-10, 0, 0, 0, 0, 0, 0, -10},
				{-20, -10, -10, -10, -10, -10, -10, -20}
			};

			_blackRookPieceSquare = new[,]
			{
				{0, 0, 0, 0, 0, 0, 0, 0},
				{5, 10, 10, 10, 10, 10, 10, 5},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{0, 0, 0, 5, 5, 0, 0, 0}
			};

			_whiteRookPieceSquare = new[,]
			{
				{0, 0, 0, 5, 5, 0, 0, 0},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{-5, 0, 0, 0, 0, 0, 0, -5},
				{5, 10, 10, 10, 10, 10, 10, 5},
				{0, 0, 0, 0, 0, 0, 0, 0}
			};

			_blackQueenPieceSquare = new[,]
			{
				{-20, -10, -10, -5, -5, -10, -10, -20},
				{-10, 0, 0, 0, 0, 0, 0, -10},
				{-10, 0, 5, 5, 5, 5, 0, -10},
				{-5, 0, 5, 5, 5, 5, 0, -5},
				{0, 0, 5, 5, 5, 5, 0, -5},
				{-10, 5, 5, 5, 5, 5, 0, -10},
				{-10, 0, 5, 0, 0, 0, 0, -10},
				{-20, -10, -10, -5, -5, -10, -10, -20}
			};

			_whiteQueenPieceSquare = new[,]
			{
				{-20, -10, -10, -5, -5, -10, -10, -20},
				{-10, 0, 5, 0, 0, 0, 0, -10},
				{-10, 5, 5, 5, 5, 5, 0, -10},
				{0, 0, 5, 5, 5, 5, 0, -5},
				{-5, 0, 5, 5, 5, 5, 0, -5},
				{-10, 0, 5, 5, 5, 5, 0, -10},
				{-10, 0, 0, 0, 0, 0, 0, -10},
				{-20, -10, -10, -5, -5, -10, -10, -20}
			};

			_blackKingMiddleGamePieceSquare = new[,]
			{
				{-30, -40, -40, -50, -50, -40, -40, -30},
				{-30, -40, -40, -50, -50, -40, -40, -30},
				{-30, -40, -40, -50, -50, -40, -40, -30},
				{-30, -40, -40, -50, -50, -40, -40, -30},
				{-20, -30, -30, -40, -40, -30, -30, -20},
				{-10, -20, -20, -20, -20, -20, -20, -10},
				{20, 20, 0, 0, 0, 0, 20, 20},
				{20, 30, 10, 0, 0, 10, 30, 20}
			};

			_whiteKingMiddlePieceSquare = new[,]
			{
				{20, 30, 10, 0, 0, 10, 30, 20},
				{20, 20, 0, 0, 0, 0, 20, 20},
				{-10, -20, -20, -20, -20, -20, -20, -10},
				{-20, -30, -30, -40, -40, -30, -30, -20},
				{-30, -40, -40, -50, -50, -40, -40, -30},
				{-30, -40, -40, -50, -50, -40, -40, -30},
				{-30, -40, -40, -50, -50, -40, -40, -30},
				{-30, -40, -40, -50, -50, -40, -40, -30},
			};

			_blackKingEndGamePieceSquare = new[,]
			{
				{-50, -40, -30, -20, -20, -30, -40, -50},
				{-30, -20, -10, 0, 0, -10, -20, -30},
				{-30, -10, 20, 30, 30, 20, -10, -30},
				{-30, -10, 30, 40, 40, 30, -10, -30},
				{-30, -10, 30, 40, 40, 30, -10, -30},
				{-30, -10, 20, 30, 30, 20, -10, -30},
				{-30, -30, 0, 0, 0, 0, -30, -30},
				{-50, -30, -30, -30, -30, -30, -30, -50}
			};

			_whiteKingEndGamePieceSquare = new[,]
			{
				{-50, -30, -30, -30, -30, -30, -30, -50},
				{-30, -30, 0, 0, 0, 0, -30, -30},
				{-30, -10, 20, 30, 30, 20, -10, -30},
				{-30, -10, 30, 40, 40, 30, -10, -30},
				{-30, -10, 30, 40, 40, 30, -10, -30},
				{-30, -10, 20, 30, 30, 20, -10, -30},
				{-30, -20, -10, 0, 0, -10, -20, -30},
				{-50, -40, -30, -20, -20, -30, -40, -50}
			};

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

		private int GetCellCost(Cell cell, bool isEndedGame)
		{
			var result = 0;
			var x = ConvertPositionXToInt(cell.Position.X);
			var y = ConvertPositionYToInt(cell.Position.Y);
			switch (cell.Figure.Type)
			{
					case FigureType.Pawn:
					result = cell.Figure.Color == Color.Black ? _blackPawnPieceSquare[x, y] : _whitePawnPieceSquare[x, y];
					break;
					case FigureType.Knight:
					result = cell.Figure.Color == Color.Black ? _blackKnightPieceSquare[x, y] : _whiteKnightPieceSquare[x, y];
					break;
					case FigureType.Bishop:
					result = cell.Figure.Color == Color.Black ? _blackBishopPieceSquare[x, y] : _whiteBishopPieceSquare[x, y];
					break;
					case FigureType.Rook:
					result = cell.Figure.Color == Color.Black ? _blackRookPieceSquare[x, y] : _whiteRookPieceSquare[x, y];
					break;
					case FigureType.Queen:
					result = cell.Figure.Color == Color.Black ? _blackQueenPieceSquare[x, y] : _whiteQueenPieceSquare[x, y];
					break;
					case FigureType.King:
					result = cell.Figure.Color == Color.Black
						? (isEndedGame ? _blackKingEndGamePieceSquare[x, y] : _blackKingMiddleGamePieceSquare[x, y])
						: (isEndedGame ? _whiteKingEndGamePieceSquare[x, y] : _whiteKingMiddlePieceSquare[x, y]);
					break;
			}
			return result;
		}

		public int GetCostFigure(FigureType type)
		{
			return _costFigures[type];
		}

		public int Evaluation(Color color, int legalMoves, int attackMoves, bool isOpponentShah, bool isOpponentCheckMat)
		{
			var costOfFigures = 0;

			var figures =
				Board.Cast<Cell>()
					.AsEnumerable()
					.Where(cell => cell.Figure != null && cell.Figure.Color == color).ToList();

			var figuresCount = figures.Count;
			figures.ForEach(cell => costOfFigures += GetCostFigure(cell.Figure.Type) + GetCellCost(cell, figuresCount < 8));

			return costOfFigures + (isOpponentShah ? 70 : 0) + (isOpponentCheckMat ? 4000 : 0) + (int) (legalMoves * 0.5) + attackMoves;
		}

		public void UndoLastMove()
		{
			if (_oldBoards.Count != 0)
			{
				DeserializeBoard(_oldBoards.Pop());
			}
		}

		public IEnumerable<Cell> GetCellOfFiguresByColor(Color color)
		{
			return Board.Cast<Cell>().AsEnumerable().
					Where(cell => cell.Figure != null && cell.Figure.Color == color).ToList();
		}

		public void SetFigureByPosition(Figure figure, Position position)
		{
			_oldBoards.Push(SerializedBoard());
			GetCellByPosition(position).Figure = figure;
		}

		public void ChangeThePositionOfTheFigure(Position from, Position to)
		{
			_oldBoards.Push(SerializedBoard());
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

		public Color GetOppositeColor(Color color)
		{
			return color == Color.Black ? Color.White : Color.Black;
		}
	}
}
