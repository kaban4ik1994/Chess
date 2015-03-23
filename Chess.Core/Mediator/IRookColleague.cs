using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface IRookColleague
    {
        bool Move(Position from, Position to, Chessboard chessboard);
    }
}