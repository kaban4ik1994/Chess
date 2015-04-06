using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorKnight
    {
        Figure FactoryMethod(Color color);
    }
}