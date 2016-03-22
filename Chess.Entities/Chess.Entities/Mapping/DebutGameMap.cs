using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
    public class DebutGameMap : EntityTypeConfiguration<DebutGame>
    {
        public DebutGameMap()
        {
            HasKey(game => game.Id);
            Property(game => game.Id);
            Property(game => game.Log);
            Property(game => game.NextMove);
            Property(game => game.TotalBlackWinGames);
            Property(game => game.TotalGame);
            Property(game => game.TotalWhiteWinGames);
            Property(game => game.WhiteWinPercent);
            Property(game => game.BlackWinPercent);
            Property(game => game.MoveColor);

            ToTable("DebutGames");
        }
    }
}
