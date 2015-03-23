using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.FactoryFigures
{
    public interface ICreatorKnight
    {
        Figure FactoryMethod(Color color);
    }
}