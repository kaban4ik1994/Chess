using Chess.Core.DebutDb;

namespace Chess.DebutGameAnalyzer
{
	class Program
	{
		static void Main(string[] args)
		{
		    var debutDbLoader = new DebutDbLoader();
            debutDbLoader.Load(0, 1).ForEach(game =>
            {
                var a = 1;
            });
		}
	}
}
