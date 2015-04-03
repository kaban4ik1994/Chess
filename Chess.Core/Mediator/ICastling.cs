using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface ICastling
    {
        void ShortCastling(Color colorOfKing, IChessboard chessboard);
        void LongCastling(Color colorOfKing, IChessboard chessboard);
        bool IsMoveIsShortCastling(Position from, Position to, IChessboard chessboard);
        bool IsMoveIsLongCastling(Position from, Position to, IChessboard chessboard);
        bool IsPossibleToMakeShortCastling(Color colorOfKing, IChessboard chessboard);
        bool IsPossibleToMakeLongCastling(Color colorOfKing, IChessboard chessboard);

    }
}
