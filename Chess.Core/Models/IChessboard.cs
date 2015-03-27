using Chess.Core.FactoryFigures;

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
    }
}