using System.Collections.Generic;
using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface IMoveMediator
    {
        bool Send(Position from, Position to, IChessboard chessboard);
        IEnumerable<Position> GetAttackMovesByColor(Color color, IChessboard chessboard);
    }
}
