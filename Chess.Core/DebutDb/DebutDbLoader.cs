using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ilf.pgn;
using ilf.pgn.Data;

namespace Chess.Core.DebutDb
{
	public class DebutDbLoader
	{
		public List<Game> AnalyzeGames { get; set; }
		private readonly PgnReader _pgnReader;
		public DebutDbLoader()
		{
			_pgnReader = new PgnReader();
			AnalyzeGames = new List<Game>();
			Directory.GetFiles("../../../Chess.Core/DebutDb/DbFiles").ToList().ForEach(s => AnalyzeGames.AddRange(_pgnReader.ReadGamesFromFile(s)));
		} 

		public int test()
		{

			return 0;

		}
	}
}
