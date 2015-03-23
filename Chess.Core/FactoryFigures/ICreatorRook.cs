using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorRook
    {
        Figure FactoryMethod(Color color);
    }
}