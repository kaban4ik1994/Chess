using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
    public class PlayerMap : EntityTypeConfiguration<Player>
    {
        public PlayerMap()
        {
            HasKey(player => player.Id);
            Property(player => player.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(player => player.IdentityNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(player => player.IsBot).IsRequired();

            ToTable("Players");

            HasOptional(item => item.Bot).WithRequired(game => game.Player).WillCascadeOnDelete(false);

            HasOptional(item => item.User).WithRequired(game => game.Player).WillCascadeOnDelete(false);

            HasMany(item => item.Invitations)
                .WithRequired(invitation => invitation.Invitator)
                .HasForeignKey(invitation => invitation.InvitatorId)
                .WillCascadeOnDelete(false);

            HasMany(item => item.Acceptions)
                .WithOptional(invitation => invitation.Acceptor)
                .HasForeignKey(invitation => invitation.AcceptorId)
                .WillCascadeOnDelete(false);

            HasMany(item => item.WatchedGames)
                .WithRequired(game => game.Player)
                .HasForeignKey(game => game.PlayerId)
                .WillCascadeOnDelete(false);
        }
    }
}
