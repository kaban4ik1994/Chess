﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public abstract class FigureColleague
    {
        public abstract bool Move(Position from, Position to, IChessboard chessboard);
        public abstract IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard);
        public abstract IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard);

        public virtual Task<IEnumerable<Position>> GetPossibleMovesAsync(Position figurePosition,
            IChessboard chessboard)
        {
            return Task.FromResult(GetPossibleMoves(figurePosition, chessboard));
        }

        public virtual Task<IEnumerable<Position>> GetAttackMovesAsync(Position figurePosition, IChessboard chessboard)
        {
            return Task.FromResult(GetAttackMoves(figurePosition, chessboard));
        }
    }
}