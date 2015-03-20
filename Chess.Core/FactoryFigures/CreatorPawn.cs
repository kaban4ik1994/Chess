using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.FactoryFigures
{
    public class CreatorPawn : Creator
    {
        public override Figure FactoryMethod(Color color)
        {
            return new Figure { Color = color, Type = FigureType.Pawn };
        }
    }
}
