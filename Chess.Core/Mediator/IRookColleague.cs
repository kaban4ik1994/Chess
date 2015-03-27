﻿using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface IRookColleague
    {
        bool Move(Position from, Position to, IChessboard chessboard);
        IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard);
    }
}