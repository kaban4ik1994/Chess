using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorPawn
    {
        Figure FactoryMethod(Color color);
    }
}