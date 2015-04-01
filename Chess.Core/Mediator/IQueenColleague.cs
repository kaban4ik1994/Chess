using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface IQueenColleague
    {
        bool Move(Position from, Position to, IChessboard chessboard);
        IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard);
        IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard);
        Task<IEnumerable<Position>> GetPossibleMovesAsync(Position figurePosition, IChessboard chessboard);
        Task<IEnumerable<Position>> GetAttackMovesAsync(Position figurePosition, IChessboard chessboard);
    }
}