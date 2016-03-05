using System.Collections.Generic;
using Chess.Core.FactoryFigures;
using Chess.Enums;

namespace Chess.Core.Models
{
	public interface IChessboard
	{
		Cell[,] Board { get; set; }
		ICreatorBishop CreatorBishop { get; }
		ICreatorKing CreatorKing { get; }
		ICreatorKnight CreatorKnight { get; }
		ICreatorPawn CreatorPawn { get; }
		ICreatorQueen CreatorQueen { get; }
		ICreatorRook CreatorRook { get; }
		IEnumerable<Cell> GetCellOfFiguresByColor(Color color);
		void SetFigureByPosition(Figure figure, Position position);
		void ChangeThePositionOfTheFigure(Position from, Position to);
		void InitNewGame();
		Cell GetCellByPosition(Position position);
		Figure GetFigureByPosition(Position position);
		int ConvertPositionXToInt(char x);
		int ConvertPositionYToInt(int y);
		char ConvertIntToPositionX(int x);
		int ConvertIntToPositionY(int y);
		char IncrementX(char x);
		char DecrementX(char x);
		int IncrementY(int y);
		int DecrementY(int y);
		string SerializedBoard();
		void DeserializeBoard(string value);
		bool IsValidPositionAndEmptyCell(Position position);
		bool IsValidPosition(Position position);
		void UndoLastMove();
		int Evaluation(Color color, int legalMoves, int attackMoves, bool isOpponentShah, bool isOpponentCheckMat);
		Color GetOppositeColor(Color color);
	}
}