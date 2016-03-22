using System.Collections.Generic;
using System.IO;
using System.Linq;
using ilf.pgn;
using ilf.pgn.Data;

namespace Chess.Core.DebutDb
{
    public class DebutDbLoader
    {
        private readonly PgnReader _pgnReader;
        private readonly string _debutDbPath;

        public DebutDbLoader(string debutDbPath = "../../../Chess.Core/DebutDb/DbFiles")
        {
            _debutDbPath = debutDbPath;
            _pgnReader = new PgnReader();
        }

        public List<Game> Load(int skip, int take)
        {
            var result = new List<Game>();
            Directory.GetFiles(_debutDbPath)
                .ToList()
                .Skip(skip)
                .Take(take)
                .ToList()
                .ForEach(s => result.AddRange(_pgnReader.ReadGamesFromFile(s)));
            return result;
        }

        public int CountOfFiles()
        {
            return Directory.GetFiles(_debutDbPath).Count();
            
        }
    }
}
