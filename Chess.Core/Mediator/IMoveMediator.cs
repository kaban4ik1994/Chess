using System.Collections.Generic;
using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.Mediator
{
    public interface IMoveMediator
    {
        MoveStatus Send(Position from, Position to, IChessboard chessboard, Color color);
        IEnumerable<Position> GetAttackMovesByColor(Color color, IChessboard chessboard);
    }
}
