using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorRook
    {
        Figure FactoryMethod(Color color);
    }
}