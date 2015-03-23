using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface IBishopColleague
    {
        bool Move(Position from, Position to, Chessboard chessboard);
    }
}