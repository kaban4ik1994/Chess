﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class KingColleague : FigureColleague, IKingColleague
    {
        public override bool Move(Position from, Position to, IChessboard chessboard)
        {
            //TODO logic
            return true;
        }

        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IEnumerable<Position>> GetPossibleMovesAsync(Position figurePosition, IChessboard chessboard)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IEnumerable<Position>> GetAttackMovesAsync(Position figurePosition, IChessboard chessboard)
        {
            throw new System.NotImplementedException();
        }
    }
}
