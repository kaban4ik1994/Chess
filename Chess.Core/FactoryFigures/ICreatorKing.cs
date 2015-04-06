using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorKing
    {
        Figure FactoryMethod(Color color);
    }
}