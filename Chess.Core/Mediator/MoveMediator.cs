using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class MoveMediator : Mediator
    {

        public PawnColleague PawnColleague;
        public QueenColleague QueenColleague;
        public RookColleague RookColleague;
        public KingColleague KingColleague;
        public KnightColleague KnightColleague;
        public BishopColleague BishopColleague;

        public override bool Send(Position from, Position to, Chessboard chessboard, FigureColleague colleague)
        {
            if (PawnColleague == colleague)
            {
                return PawnColleague.Move(from, to, chessboard);
            }

            if (QueenColleague == colleague)
            {
                return QueenColleague.Move(from, to, chessboard);
            }

            if (RookColleague == colleague)
            {
                return RookColleague.Move(from, to, chessboard);
            }

            if (KingColleague == colleague)
            {
                return KingColleague.Move(from, to, chessboard);
            }

            if (KnightColleague == colleague)
            {
                return KnightColleague.Move(from, to, chessboard);
            }

            if (BishopColleague == colleague)
            {
                return BishopColleague.Move(from, to, chessboard);
            }
            return false;
        }

    }
}
