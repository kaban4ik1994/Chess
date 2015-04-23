using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public abstract class FigureColleague
    {

        public virtual bool Move(Position from, Position to, IChessboard chessboard)
        {

            var possibleMoves = GetPossibleMovesAsync(from, chessboard);
            var attackMoves = GetAttackMovesAsync(from, chessboard);


            if (possibleMoves.Result.Any(x => x.Equals(to)) || attackMoves.Result.Any(x => x.Equals(to)))
            {
                chessboard.ChangeThePositionOfTheFigure(from, to);
                return true;
            }

            return false;
        }

        protected IEnumerable<Position> GetPossibleMovesOnTheLineXAtUp(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);
            var isEndLine = false;
            while (!isEndLine)
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                    result.Add(new Position(testPosition));
                else isEndLine = true;
            }
            return result;
        }

        protected IEnumerable<Position> GetPossibleMovesOnTheLineYAtUp(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);
            var isEndLine = false;
            while (!isEndLine)
            {
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                    result.Add(new Position(testPosition));
                else isEndLine = true;
            }
            return result;
        }

        protected IEnumerable<Position> GetPossibleMovesOnTheLineXAtDown(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);
            var isEndLine = false;
            while (!isEndLine)
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                    result.Add(new Position(testPosition));
                else isEndLine = true;
            }
            return result;
        }

        protected IEnumerable<Position> GetPossibleMovesOnTheLineYAtDown(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);
            var isEndLine = false;
            while (!isEndLine)
            {
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                    result.Add(new Position(testPosition));
                else isEndLine = true;
            }
            return result;
        }

        protected IEnumerable<Position> GetPossibleMovesOnTheLineXAtUpAndYAtUp(Position figurePosition,
            IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine)
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                    result.Add(new Position(testPosition));
                else isEndLine = true;
            }

            return result;
        }

        protected IEnumerable<Position> GetPossibleMovesOnTheLineXAtUpAndYAtDown(Position figurePosition,
            IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine)
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                    result.Add(new Position(testPosition));
                else isEndLine = true;
            }

            return result;
        }

        protected IEnumerable<Position> GetPossibleMovesOnTheLineXAtDownAndYAtDown(Position figurePosition,
            IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine)
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                    result.Add(new Position(testPosition));
                else isEndLine = true;
            }

            return result;
        }

        protected IEnumerable<Position> GetPossibleMovesOnTheLineXAtDownAndYAtUp(Position figurePosition,
            IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine)
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                    result.Add(new Position(testPosition));
                else isEndLine = true;
            }

            return result;
        }

        protected IEnumerable<Position> GetAttackMovesOnTheLineXAtUp(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var isEndLine = false;
            while (!isEndLine)
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                if (chessboard.IsValidPosition(testPosition))
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

        protected IEnumerable<Position> GetAttackMovesOnTheLineYAtUp(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var isEndLine = false;
            while (!isEndLine)
            {
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (chessboard.IsValidPosition(testPosition))
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

        protected IEnumerable<Position> GetAttackMovesOnTheLineXAtDown(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var isEndLine = false;
            while (!isEndLine)
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                if (chessboard.IsValidPosition(testPosition))
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

        protected IEnumerable<Position> GetAttackMovesOnTheLineYAtDown(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var isEndLine = false;
            while (!isEndLine)
            {
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (chessboard.IsValidPosition(testPosition))
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

        protected IEnumerable<Position> GetAttackMovesOnTheLineXAtUpAndYAtUp(Position figurePosition,
            IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine)
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (chessboard.IsValidPosition(testPosition))
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

        protected IEnumerable<Position> GetAttackMovesOnTheLineXAtUpAndYAtDown(Position figurePosition,
            IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine)
            {
                testPosition.X = chessboard.IncrementX(testPosition.X);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (chessboard.IsValidPosition(testPosition))
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

        protected IEnumerable<Position> GetAttackMovesOnTheLineXAtDownAndYAtDown(Position figurePosition,
            IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine)
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                if (chessboard.IsValidPosition(testPosition))
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

        protected IEnumerable<Position> GetAttackMovesOnTheLineXAtDownAndYAtUp(Position figurePosition,
            IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);
            var isEndLine = false;

            while (!isEndLine) //x down, y up
            {
                testPosition.X = chessboard.DecrementX(testPosition.X);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                if (chessboard.IsValidPosition(testPosition))
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