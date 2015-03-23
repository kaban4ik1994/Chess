using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface IMoveMediator
    {
        bool Send(Position from, Position to, Chessboard chessboard);
    }
}
