using Chess.Enums;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class Bot:Entity
    {
        public long Id { get; set; }
        public BotType Type { get; set; }

        public Player Player { get; set; }
    }
}
