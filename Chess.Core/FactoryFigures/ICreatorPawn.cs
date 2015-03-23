using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorPawn
    {
        Figure FactoryMethod(Color color);
    }
}