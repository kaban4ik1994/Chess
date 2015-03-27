using Newtonsoft.Json;

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

        [JsonProperty("Figure")]
        public Figure Figure { get; set; }
        [JsonProperty("Position")]
        public Position Position { get; set; }
    }
}
