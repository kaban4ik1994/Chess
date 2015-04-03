using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class BishopColleague : FigureColleague, IBishopColleague
    {
        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine) //x up, y up
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (testPosition.X != ' '
                    && testPosition.Y != -1
                    && chessboard.GetFigureByPosition(testPosition) == null)
                {
                    result.Add(new Position(testPosition));
                }
                else isEndLine = true;
            }

            testPosition = new Position(figurePosition);
            isEndLine = false;
            while (!isEndLine) //x up, y down
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (testPosition.X != ' '
                    && testPosition.Y != -1
                    && chessboard.GetFigureByPosition(testPosition) == null)
                {
                    result.Add(new Position(testPosition));
                }
                else isEndLine = true;
            }

            testPosition = new Position(figurePosition);
            isEndLine = false;
            while (!isEndLine) //x down, y down
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (testPosition.X != ' '
                    && testPosition.Y != -1
                    && chessboard.GetFigureByPosition(testPosition) == null)
                {
                    result.Add(new Position(testPosition));
                }
                else isEndLine = true;
            }

            testPosition = new Position(figurePosition);
            isEndLine = false;
            while (!isEndLine) //x down, y up
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (testPosition.X != ' '
                    && testPosition.Y != -1
                    && chessboard.GetFigureByPosition(testPosition) == null)
                {
                    result.Add(new Position(testPosition));
                }
                else isEndLine = true;
            }

            return result;
        }

        public override IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine) //x up, y up
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (testPosition.X != ' ' && testPosition.Y != -1)
                {
                    var testFigure = chessboard.GetFigureByPosition(testPosition);
                    if (testFigure != null)
                    {
                        if (testFigure.Color != figure.Color)
                        {
                            result.Add(new Position(testPosition));
                            isEndLine = true;
                        }
                        else
                        {
                            isEndLine = true;
                        }
                    }
                }
                else isEndLine = true;
            }

            testPosition = new Position(figurePosition);
            isEndLine = false;
            while (!isEndLine) //x up, y down
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (testPosition.X != ' ' && testPosition.Y != -1)
                {
                    var testFigure = chessboard.GetFigureByPosition(testPosition);
                    if (testFigure != null)
                    {
                        if (testFigure.Color != figure.Color)
                        {
                            result.Add(new Position(testPosition));
                            isEndLine = true;
                        }
                        else
                        {
                            isEndLine = true;
                        }
                    }
                }
                else isEndLine = true;
            }

            testPosition = new Position(figurePosition);
            isEndLine = false;
            while (!isEndLine) //x down, y down
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (testPosition.X != ' ' && testPosition.Y != -1)
                {
                    var testFigure = chessboard.GetFigureByPosition(testPosition);
                    if (testFigure != null)
                    {
                        if (testFigure.Color != figure.Color)
                        {
                            result.Add(new Position(testPosition));
                            isEndLine = true;
                        }
                        else
                        {
                            isEndLine = true;
                        }
                    }
                }
                else isEndLine = true;
            }

            testPosition = new Position(figurePosition);
            isEndLine = false;
            while (!isEndLine) //x down, y up
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (testPosition.X != ' '
                    && testPosition.Y != -1)
                {
                    var testFigure = chessboard.GetFigureByPosition(testPosition);
                    if (testFigure != null)
                    {
                        if (testFigure.Color != figure.Color)
                        {
                            result.Add(new Position(testPosition));
                            isEndLine = true;
                        }
                        else
                        {
                            isEndLine = true;
                        }
                    }
                }
                else isEndLine = true;
            }

            return result;
        }
    }
}
