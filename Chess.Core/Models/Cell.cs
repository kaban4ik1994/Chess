namespace Chess.Core.Models
{
    public class Cell
    {
        public Cell()
        {

        }

        public Cell(Cell cell)
        {
            Figure = new Figure(cell.Figure);
            Position = new Position(cell.Position);
        }

        public Figure Figure { get; set; }
        public Position Position { get; set; }
    }
}
