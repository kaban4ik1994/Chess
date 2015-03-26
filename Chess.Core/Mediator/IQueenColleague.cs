﻿using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public interface IQueenColleague
    {
        bool Move(Position from, Position to, Chessboard chessboard);
        IEnumerable<Position> GetPossibleMoves(Position figurePosition, Chessboard chessboard);
    }
}