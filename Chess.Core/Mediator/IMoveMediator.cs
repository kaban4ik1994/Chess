using System.Collections.Generic;
using Chess.Core.Bot;
using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.Mediator
{
    public interface IMoveMediator
    {
        MoveStatus Send(Position from, Position to, IChessboard chessboard, Color color);
        IEnumerable<Position> GetAttackMovesByColor(Color color, IChessboard chessboard);
        IEnumerable<Position> GetPossibleMovesByColor(Color color, IChessboard chessboard);
        IEnumerable<ExtendedPosition> GetExtendedAttackMovesByColor(Color color, IChessboard chessboard);
        IEnumerable<ExtendedPosition> GetExtendedPossibleMovesByColor(Color color, IChessboard chessboard);

    }
}
