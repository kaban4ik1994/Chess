using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class KnightColleague : FigureColleague, IKnightColleague
    {

        public override bool Move(Position from, Position to, IChessboard chessboard)
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

        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);

            var testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.X = chessboard.DecrementX(testPosition.X);

            if (testPosition.X != ' ' && testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1 && chessboard.GetFigureByPosition(testPosition) == null)
            {
                result.Add(testPosition);
            }

            return result;

        }

        public override IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);

            var testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.X = chessboard.DecrementX(testPosition.X);

            if (testPosition.X != ' ' && testPosition.Y != -1
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
            {
                result.Add(testPosition);
            }

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (testPosition.X != ' ' && testPosition.Y != -1
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
            {
                result.Add(testPosition);
            }

            return result;
        }

    }
}
