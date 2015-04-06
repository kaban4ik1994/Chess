using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorQueen
    {
        Figure FactoryMethod(Color color);
    }
}