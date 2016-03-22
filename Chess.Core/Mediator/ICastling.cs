using Chess.Core.Bot;
using Chess.Core.Models;
using Chess.Enums;

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
        ExtendedPosition GetLongCastlingMove(Color color);
        ExtendedPosition GetShortCastlingMove(Color color);

    }
}
