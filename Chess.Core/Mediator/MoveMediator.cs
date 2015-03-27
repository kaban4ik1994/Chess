using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class MoveMediator : IMoveMediator
    {

        private readonly IPawnColleague _pawnColleague;
        private readonly IQueenColleague _queenColleague;
        private readonly IRookColleague _rookColleague;
        private readonly IKingColleague _kingColleague;
        private readonly IKnightColleague _knightColleague;
        private readonly IBishopColleague _bishopColleague;

        public MoveMediator(IPawnColleague pawnColleague, IQueenColleague queenColleague, IRookColleague rookColleague, IKingColleague kingColleague, IKnightColleague knightColleague, IBishopColleague bishopColleague)
        {
            _pawnColleague = pawnColleague;
            _queenColleague = queenColleague;
            _rookColleague = rookColleague;
            _kingColleague = kingColleague;
            _knightColleague = knightColleague;
            _bishopColleague = bishopColleague;
        }

        public bool Send(Position from, Position to, IChessboard chessboard)
        {
            var figureFrom = chessboard.GetFigureByPosition(from);
            if (figureFrom == null) return false;
            if (figureFrom.Type == FigureType.Pawn)
            {
                return _pawnColleague.Move(from, to, chessboard);
            }

            if (figureFrom.Type == FigureType.Queen)
            {
                return _queenColleague.Move(from, to, chessboard);
            }

            if (figureFrom.Type == FigureType.Rook)
            {
                return _rookColleague.Move(from, to, chessboard);
            }

            if (figureFrom.Type == FigureType.King)
            {
                return _kingColleague.Move(from, to, chessboard);
            }

            if (figureFrom.Type == FigureType.Knight)
            {
                return _knightColleague.Move(from, to, chessboard);
            }

            if (figureFrom.Type == FigureType.Bishop)
            {
                return _bishopColleague.Move(from, to, chessboard);
            }
            return false;
        }

    }
}
