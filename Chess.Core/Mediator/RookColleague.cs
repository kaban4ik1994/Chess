using System.Collections.Generic;
using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class RookColleague : FigureColleague, IRookColleague
    {
        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var isEndLine = false;
            var testPosition = new Position(figurePosition);

            while (!isEndLine) //y up
            {
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
                {
                    result.Add(new Position(testPosition));
                }
                else isEndLine = true;
            }

            testPosition = new Position(figurePosition);
            isEndLine = false;
            while (!isEndLine) //y down
            {
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
                {
                    result.Add(new Position(testPosition));
                }
                else isEndLine = true;
            }

            testPosition = new Position(figurePosition);
            isEndLine = false;
            while (!isEndLine) //x up
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                if (testPosition.X != ' ' && chessboard.GetFigureByPosition(testPosition) == null)
                {
                    result.Add(new Position(testPosition));
                }
                else isEndLine = true;
            }

            testPosition = new Position(figurePosition);
            isEndLine = false;
            while (!isEndLine) //x down
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                if (testPosition.X != ' ' && chessboard.GetFigureByPosition(testPosition) == null)
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
            var isEndLine = false;
            var testPosition = new Position(figurePosition);

            while (!isEndLine) //y up
            {
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (testPosition.Y != -1)
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
            while (!isEndLine) //y down
            {
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (testPosition.Y != -1)
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
            while (!isEndLine) //x up
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                if (testPosition.X != ' ')
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
            while (!isEndLine) //x down
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                if (testPosition.X != ' ')
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
