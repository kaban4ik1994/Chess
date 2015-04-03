using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class KingColleague : FigureColleague, IKingColleague
    {
        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var testPosition = new Position(figurePosition);

            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                result.Add(new Position(testPosition));

            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                result.Add(new Position(testPosition));

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                result.Add(new Position(testPosition));

            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                result.Add(new Position(testPosition));

            testPosition = new Position(figurePosition);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                result.Add(new Position(testPosition));

            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                result.Add(new Position(testPosition));

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                result.Add(new Position(testPosition));

            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (chessboard.IsValidPositionAndEmptyCell(testPosition))
                result.Add(new Position(testPosition));

            return result;
        }

        public override IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);

            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (chessboard.IsValidPosition(testPosition)
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                result.Add(new Position(testPosition));

            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            if (chessboard.IsValidPosition(testPosition)
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                result.Add(new Position(testPosition));

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.IncrementY(testPosition.Y);
            if (chessboard.IsValidPosition(testPosition)
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                result.Add(new Position(testPosition));

            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (chessboard.IsValidPosition(testPosition)
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                result.Add(new Position(testPosition));

            testPosition = new Position(figurePosition);
            testPosition.X = chessboard.DecrementX(testPosition.X);
            if (chessboard.IsValidPosition(testPosition)
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                result.Add(new Position(testPosition));

            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            if (chessboard.IsValidPosition(testPosition)
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                result.Add(new Position(testPosition));

            testPosition = new Position(figurePosition);
            testPosition.Y = chessboard.DecrementY(testPosition.Y);
            if (chessboard.IsValidPosition(testPosition)
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                result.Add(new Position(testPosition));

            testPosition.X = chessboard.IncrementX(testPosition.X);
            if (chessboard.IsValidPosition(testPosition)
                && chessboard.GetFigureByPosition(testPosition) != null
                && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                result.Add(new Position(testPosition));

            return result;
        }
    }
}
