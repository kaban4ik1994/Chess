using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class PawnColleague : FigureColleague, IPawnColleague
    {
        public override bool Move(Position from, Position to, Chessboard chessboard)
        {
            var figureTo = chessboard.GetFigureByPosition(to);
            var figureFrom = chessboard.GetFigureByPosition(from);

            if (figureTo == null)
            {
                if (from.X == to.X)
                {
                    if (figureFrom.Color == Color.Black)
                    {
                        if (to.Y - from.Y == -1)
                        {
                            chessboard.ChangeThePositionOfTheFigure(from, to);
                            return true;
                        }

                        if (to.Y - from.Y == -2 && from.Y == 7)
                        {
                            chessboard.ChangeThePositionOfTheFigure(from, to);
                            return true;
                        }

                        return false;
                    }

                    if (figureFrom.Color == Color.White)
                    {
                        if (to.Y - from.Y == 1)
                        {
                            chessboard.ChangeThePositionOfTheFigure(from, to);
                            return true;
                        }

                        if (to.Y - from.Y == 2 && from.Y == 2)
                        {
                            chessboard.ChangeThePositionOfTheFigure(from, to);
                            return true;
                        }

                        return false;
                    }
                }
            }

            else
            {
                if (figureTo.Color == figureFrom.Color) return false;

                var fromPositionX = chessboard.ConvertPositionXToInt(from.X);
                var fromPositionY = chessboard.ConvertPositionYToInt(from.Y);
                var toPositionX = chessboard.ConvertPositionXToInt(to.X);
                var toPositionY = chessboard.ConvertPositionYToInt(to.Y);

                if (figureFrom.Color == Color.Black)
                {
                    if (fromPositionX + 1 == toPositionX
                        || fromPositionX - 1 == toPositionX
                        && fromPositionY - 1 == toPositionY)
                    {
                        chessboard.ChangeThePositionOfTheFigure(from, to);
                        return true;
                    }
                }

                if (figureFrom.Color == Color.White)
                {
                    if (fromPositionX + 1 == toPositionX
                        || fromPositionX - 1 == toPositionX
                        && fromPositionY + 1 == toPositionY)
                    {
                        chessboard.ChangeThePositionOfTheFigure(from, to);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
