using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class PawnColleague : FigureColleague, IPawnColleague
    {

        public override bool Move(Position from, Position to, IChessboard chessboard)
        {

            var possibleMoves = GetPossibleMovesAsync(from, chessboard);
            var attackMoves = GetAttackMovesAsync(from, chessboard);


            if (possibleMoves.Result.Any(x => x.Equals(to)) || attackMoves.Result.Any(x => x.Equals(to)))
            {
                chessboard.ChangeThePositionOfTheFigure(from, to);
                if (to.Y == 8 || to.Y == 1) ChangeToQueen(to, chessboard);
                return true;
            }

            return false;
        }

        public void ChangeToQueen(Position figurePosition, IChessboard chessboard)
        {
            var figure = chessboard.GetFigureByPosition(figurePosition);
            chessboard.SetFigureByPosition(new Figure { Color = figure.Color, Type = FigureType.Queen }, figurePosition);
        }


        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
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

            }

            return result;
        }

        public override IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var testPosition2 = new Position(figurePosition);

            if (figure.Color == Color.White)
            {
                testPosition.Y += 2;
                testPosition2.Y++;

                testPosition2.X = chessboard.IncrementX(testPosition2.X);

                if (chessboard.ConvertPositionXToInt(testPosition2.X) <= chessboard.ConvertPositionXToInt('H')
                    && chessboard.GetFigureByPosition(testPosition2) != null
                    && chessboard.GetFigureByPosition(testPosition2).Color != figure.Color)
                {
                    result.Add(new Position(testPosition2));
                }

                testPosition2.X = chessboard.DecrementX(testPosition2.X);
                testPosition2.X = chessboard.DecrementX(testPosition2.X);

                if (chessboard.ConvertPositionXToInt(testPosition2.X) >= chessboard.ConvertPositionXToInt('A')
                    && chessboard.GetFigureByPosition(testPosition2) != null
                    && chessboard.GetFigureByPosition(testPosition2).Color != figure.Color)
                {
                    result.Add(new Position(testPosition2));
                }
            }

            else if (figure.Color == Color.Black)
            {
                testPosition.Y -= 2;
                testPosition2.Y--;

                testPosition2.X = chessboard.IncrementX(testPosition2.X);

                if (chessboard.ConvertPositionXToInt(testPosition2.X) <= chessboard.ConvertPositionXToInt('H')
                    && chessboard.GetFigureByPosition(testPosition2) != null
                    && chessboard.GetFigureByPosition(testPosition2).Color != figure.Color)
                {
                    result.Add(new Position(testPosition2));
                }

                testPosition2.X = chessboard.DecrementX(testPosition2.X);
                testPosition2.X = chessboard.DecrementX(testPosition2.X);

                if (chessboard.ConvertPositionXToInt(testPosition2.X) >= chessboard.ConvertPositionXToInt('A')
                    && chessboard.GetFigureByPosition(testPosition2) != null
                    && chessboard.GetFigureByPosition(testPosition2).Color != figure.Color)
                {
                    result.Add(new Position(testPosition2));
                }
            }
            return result;
        }
    }
}
