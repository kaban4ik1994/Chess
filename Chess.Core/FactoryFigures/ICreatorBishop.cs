using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorBishop
    {
        Figure FactoryMethod(Color color);
    }
}