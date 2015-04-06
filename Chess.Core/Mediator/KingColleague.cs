using System.Collections.Generic;
using System.Linq;
using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.Mediator
{
    public class KingColleague : FigureColleague, IKingColleague, ICastling
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

            var colorMoveFigure = chessboard.GetFigureByPosition(from).Color;
            if (IsMoveIsLongCastling(from, to, chessboard)
                && IsPossibleToMakeLongCastling(colorMoveFigure, chessboard))
            {
                LongCastling(colorMoveFigure, chessboard);
                return true;
            }
            if (IsMoveIsShortCastling(from, to, chessboard)
                && IsPossibleToMakeShortCastling(colorMoveFigure, chessboard))
            {
                ShortCastling(colorMoveFigure, chessboard);
                return true;
            }
            return false;
        }

        public void ShortCastling(Color colorOfKing, IChessboard chessboard)
        {
            var y = colorOfKing == Color.White ? 1 : 8;
            var positionKing = new Position { X = 'E', Y = y };
            var positionRook = new Position { X = 'H', Y = y };
            chessboard.ChangeThePositionOfTheFigure(positionKing, new Position { X = 'G', Y = y });
            chessboard.ChangeThePositionOfTheFigure(positionRook, new Position { X = 'F', Y = y });
        }

        public void LongCastling(Color colorOfKing, IChessboard chessboard)
        {
            var y = colorOfKing == Color.White ? 1 : 8;
            var positionKing = new Position { X = 'E', Y = y };
            var positionRook = new Position { X = 'A', Y = y };
            chessboard.ChangeThePositionOfTheFigure(positionKing, new Position { X = 'C', Y = y });
            chessboard.ChangeThePositionOfTheFigure(positionRook, new Position { X = 'D', Y = y });
        }

        public bool IsMoveIsShortCastling(Position from, Position to, IChessboard chessboard)
        {
            var king = chessboard.GetFigureByPosition(from);
            var y = king.Color == Color.White ? 1 : 8;
            var positionKing = new Position { X = 'E', Y = y };
            var positionRook = new Position { X = 'H', Y = y };
            return from.Equals(positionKing) && to.Equals(positionRook);
        }

        public bool IsMoveIsLongCastling(Position from, Position to, IChessboard chessboard)
        {
            var king = chessboard.GetFigureByPosition(from);
            var y = king.Color == Color.White ? 1 : 8;
            var positionKing = new Position { X = 'E', Y = y };
            var positionRook = new Position { X = 'A', Y = y };
            return from.Equals(positionKing) && to.Equals(positionRook);
        }

        public bool IsPossibleToMakeShortCastling(Color colorOfKing, IChessboard chessboard)
        {
            var y = colorOfKing == Color.White ? 1 : 8;
            var positionKing = new Position { X = 'E', Y = y };
            var positionRook = new Position { X = 'H', Y = y };
            var king = chessboard.GetFigureByPosition(positionKing);
            var rook = chessboard.GetFigureByPosition(positionRook);
            if (king.IsMakeFirstMove
                || rook == null
                || rook.Color != colorOfKing
                || rook.IsMakeFirstMove) return false;
            return chessboard.GetFigureByPosition(new Position { X = 'F', Y = y }) == null &&
                   chessboard.GetFigureByPosition(new Position { X = 'G', Y = y }) == null;
        }

        public bool IsPossibleToMakeLongCastling(Color colorOfKing, IChessboard chessboard)
        {
            var y = colorOfKing == Color.White ? 1 : 8;
            var positionKing = new Position { X = 'E', Y = y };
            var positionRook = new Position { X = 'A', Y = y };
            var king = chessboard.GetFigureByPosition(positionKing);
            var rook = chessboard.GetFigureByPosition(positionRook);
            if (king.IsMakeFirstMove
                || rook == null
                || rook.Color != colorOfKing
                || rook.IsMakeFirstMove) return false;
            return chessboard.GetFigureByPosition(new Position { X = 'B', Y = y }) == null &&
                   chessboard.GetFigureByPosition(new Position { X = 'C', Y = y }) == null &&
                   chessboard.GetFigureByPosition(new Position { X = 'D', Y = y }) == null;
        }

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
