using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorBishop
    {
        Figure FactoryMethod(Color color);
    }
}