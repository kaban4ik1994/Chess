using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorQueen
    {
        Figure FactoryMethod(Color color);
    }
}