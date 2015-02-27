using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
    public class GameMap : EntityTypeConfiguration<Game>
    {
        public GameMap()
        {
            HasKey(game => game.Id);
            Property(game => game.Id);
            Property(game => game.FirstPlayerGameTime).HasColumnType("datetime2").IsRequired();
            Property(game => game.GameIdentificator).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(game => game.GameStartDate).HasColumnType("datetime2").IsRequired();
            Property(game => game.IsEnded).IsRequired();
            Property(game => game.SecondPlayerGameTime).HasColumnType("datetime2").IsRequired();

            ToTable("Games");

            HasRequired(item => item.Invitation)
                .WithOptional(player => player.Game)
                .WillCascadeOnDelete(false);

            HasMany(item => item.Watchers)
                .WithRequired(game => game.Game)
                .HasForeignKey(game => game.GameId)
                .WillCascadeOnDelete(false);

            HasMany(item => item.GameLogs)
                .WithRequired(log => log.Game)
                .HasForeignKey(log => log.GameId)
                .WillCascadeOnDelete(false);
        }
    }
}
