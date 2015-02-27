using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
    public class PlayerGamesMap : EntityTypeConfiguration<PlayerGame>
    {
        public PlayerGamesMap()
        {
            HasKey(player => player.Id);
            Property(player => player.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(game => game.LookTime).HasColumnType("datetime2").IsRequired();
            
            ToTable("PlayerGames");

            HasRequired(item => item.Player)
                .WithMany(player => player.WatchedGames)
                .HasForeignKey(game => game.PlayerId)
                .WillCascadeOnDelete(false);

            HasRequired(item => item.Player)
                .WithMany(player => player.WatchedGames)
                .HasForeignKey(game => game.PlayerId)
                .WillCascadeOnDelete(false);
        }
    }
}
