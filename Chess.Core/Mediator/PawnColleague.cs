﻿using System.Collections.Generic;
using System.Linq;
using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class PawnColleague : FigureColleague, IPawnColleague
    {

        public override bool Move(Position from, Position to, Chessboard chessboard)
        {

            var possibleMoves = GetPossibleMoves(from, chessboard);

            if (possibleMoves.Any(x => x.Equals(to)))
            {
                chessboard.ChangeThePositionOfTheFigure(from, to);
                return true;
            }

            return false;
        }

        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, Chessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var testPosition2 = new Position(figurePosition);

            if (figure.Color == Color.White)
            {
                testPosition.Y += 2;
                testPosition2.Y++;

                if (figurePosition.Y == 2 && chessboard.GetFigureByPosition(testPosition) == null && chessboard.GetFigureByPosition(testPosition2) == null)
                {
                    result.Add(new Position(testPosition));
                }

                if (testPosition2.Y <= 8 && chessboard.GetFigureByPosition(testPosition2) == null)
                {
                    result.Add(new Position(testPosition2));
                }

                testPosition2.X = chessboard.IncrementX(testPosition2.X);

                if (chessboard.ConvertPositionXToInt(testPosition2.X) <= chessboard.ConvertPositionXToInt('H') && chessboard.GetFigureByPosition(testPosition2) != null)
                {
                    result.Add(new Position(testPosition2));
                }

                testPosition2.X = chessboard.DecrementX(testPosition2.X);
                testPosition2.X = chessboard.DecrementX(testPosition2.X);

                if (chessboard.ConvertPositionXToInt(testPosition2.X) >= chessboard.ConvertPositionXToInt('A') && chessboard.GetFigureByPosition(testPosition2) != null)
                {
                    result.Add(new Position(testPosition2));
                }

            }

            else if (figure.Color == Color.Black)
            {
                testPosition.Y -= 2;
                testPosition2.Y--;

                if (figurePosition.Y == 7 && chessboard.GetFigureByPosition(testPosition) == null && chessboard.GetFigureByPosition(testPosition2) == null)
                {
                    result.Add(new Position(testPosition));
                }

                if (testPosition2.Y >= 1 && chessboard.GetFigureByPosition(testPosition2) == null)
                {
                    result.Add(new Position(testPosition2));
                }

                testPosition2.X = chessboard.IncrementX(testPosition2.X);

                if (chessboard.ConvertPositionXToInt(testPosition2.X) <= chessboard.ConvertPositionXToInt('H') && chessboard.GetFigureByPosition(testPosition2) != null)
                {
                    result.Add(new Position(testPosition2));
                }

                testPosition2.X = chessboard.DecrementX(testPosition2.X);
                testPosition2.X = chessboard.DecrementX(testPosition2.X);

                if (chessboard.ConvertPositionXToInt(testPosition2.X) >= chessboard.ConvertPositionXToInt('A') && chessboard.GetFigureByPosition(testPosition2) != null)
                {
                    result.Add(new Position(testPosition2));
                }
            }

            return result;
        }
    }
}