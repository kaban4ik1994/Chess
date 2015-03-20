using Chess.Core.Enums;
using Chess.Core.Models;

namespace Chess.Core.FactoryFigures
{
    public abstract class Creator
    {
        public abstract Figure FactoryMethod(Color color);
    }
}
