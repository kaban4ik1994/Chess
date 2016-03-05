using Chess.Core.Bot;
using Chess.Enums;

namespace Chess.Core.Models
{
	public class AlgorithmResultModel
	{
		public int CurrentCosts { get; set; }
		public ExtendedPosition Move { get; set; }

		public AlgorithmResultModel()
		{
			CurrentCosts = 0;
		}

		public AlgorithmResultModel(int currentCost)
		{
			CurrentCosts = currentCost;
		}

		public AlgorithmResultModel(ExtendedPosition position, int currentCosts)
		{
			Move = new ExtendedPosition { From = new Position(position.From), To = new Position(position.To) };
			CurrentCosts = currentCosts;
		}
	}
}
