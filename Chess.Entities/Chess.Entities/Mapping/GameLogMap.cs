using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
    public class GameLogMap : EntityTypeConfiguration<GameLog>
    {
        public GameLogMap()
        {
            HasKey(game => game.Id);
            Property(game => game.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(game => game.CreateDate).HasColumnType("datetime2").IsRequired();
            Property(game => game.Index).IsRequired();
            Property(game => game.Log).IsRequired();

            ToTable("GameLogs");

            HasRequired(item => item.Game)
                .WithMany(game => game.GameLogs)
                .HasForeignKey(log => log.GameId)
                .WillCascadeOnDelete(false);
        }
    }
}
