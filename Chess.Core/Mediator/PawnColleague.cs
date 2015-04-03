using System.Collections.Generic;
using System.Linq;
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
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                testPosition2.Y = chessboard.IncrementY(testPosition2.Y);

                if (figurePosition.Y == 2
                    && chessboard.IsValidPositionAndEmptyCell(testPosition)
                    && chessboard.IsValidPositionAndEmptyCell(testPosition2))
                    result.Add(new Position(testPosition));

                if (chessboard.IsValidPositionAndEmptyCell(testPosition2))
                    result.Add(new Position(testPosition2));
            }

            else if (figure.Color == Color.Black)
            {
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                testPosition2.Y = chessboard.DecrementY(testPosition2.Y);

                if (figurePosition.Y == 7
                    && chessboard.IsValidPositionAndEmptyCell(testPosition)
                    && chessboard.IsValidPositionAndEmptyCell(testPosition2))
                    result.Add(new Position(testPosition));

                if (chessboard.IsValidPositionAndEmptyCell(testPosition2))
                    result.Add(new Position(testPosition2));

            }

            return result;
        }

        public override IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();
            var figure = chessboard.GetFigureByPosition(figurePosition);
            var testPosition = new Position(figurePosition);

            if (figure.Color == Color.White)
            {

                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                testPosition.X = chessboard.IncrementX(testPosition.X);

                if (chessboard.IsValidPosition(testPosition)
                    && chessboard.GetFigureByPosition(testPosition) != null
                    && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                {
                    result.Add(new Position(testPosition));
                }

                testPosition = new Position(figurePosition);
                testPosition.Y = chessboard.IncrementY(testPosition.Y);
                testPosition.X = chessboard.DecrementX(testPosition.X);

                if (chessboard.IsValidPosition(testPosition)
                    && chessboard.GetFigureByPosition(testPosition) != null
                    && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                {
                    result.Add(new Position(testPosition));
                }
            }

            else if (figure.Color == Color.Black)
            {
                testPosition = new Position(figurePosition);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                testPosition.X = chessboard.IncrementX(testPosition.X);

                if (chessboard.IsValidPosition(testPosition)
                    && chessboard.GetFigureByPosition(testPosition) != null
                    && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                {
                    result.Add(new Position(testPosition));
                }

                testPosition = new Position(figurePosition);
                testPosition.Y = chessboard.DecrementY(testPosition.Y);
                testPosition.X = chessboard.DecrementX(testPosition.X);

                if (chessboard.IsValidPosition(testPosition)
                    && chessboard.GetFigureByPosition(testPosition) != null
                    && chessboard.GetFigureByPosition(testPosition).Color != figure.Color)
                {
                    result.Add(new Position(testPosition));
                }
            }
            return result;
        }
    }
}
