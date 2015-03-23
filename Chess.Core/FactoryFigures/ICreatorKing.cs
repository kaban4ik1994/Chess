using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorKing
    {
        Figure FactoryMethod(Color color);
    }
}