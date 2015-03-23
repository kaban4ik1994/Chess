using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface IKnightColleague
    {
        bool Move(Position from, Position to, Chessboard chessboard);
    }
}