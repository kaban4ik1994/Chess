namespace Chess.Models
{
    public class MakeMoveViewModel
    {
        public long GameId { get; set; }
        public char FromX { get; set; }
        public int FromY { get; set; }
        public char ToX { get; set; }
        public int ToY { get; set; }
    }
}
