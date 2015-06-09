using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface IPawnColleague : IFigureColleague
    {
        void ChangeToQueen(Position figurePosition, IChessboard chessboard);
    }
}