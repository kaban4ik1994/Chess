using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.FactoryFigures
{
    public class CreatorBishop : ICreatorBishop
    {
        public Figure FactoryMethod(Color color)
        {
            return new Figure { Color = color, Type = FigureType.Bishop };
        }
    }
}
