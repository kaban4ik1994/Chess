using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class Bot:Entity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public long PlayerId { get; set; }

        public Player Player { get; set; }
    }
}
