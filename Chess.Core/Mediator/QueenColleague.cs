using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class QueenColleague : FigureColleague, IQueenColleague
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
            throw new System.NotImplementedException();
        }
    }
}
