using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
   public class InvitationMap : EntityTypeConfiguration<Invitation>
    {
       public InvitationMap()
       {
           HasKey(invitation => invitation.Id);
           Property(invitation => invitation.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

           Property(invitation => invitation.CreateDate).HasColumnType("datetime2").IsRequired();
           Property(invitation => invitation.IsAccepted).IsRequired();
           Property(invitation => invitation.IsDeclined).IsRequired();

           ToTable("Invitations");

           HasOptional(item => item.Game)
               .WithRequired(game => game.Invitation)
               .WillCascadeOnDelete(false);

           HasRequired(item => item.Invitator)
               .WithMany(player => player.Invitations)
               .HasForeignKey(invitation => invitation.InvitatorId)
               .WillCascadeOnDelete(false);

           HasOptional(item => item.Acceptor)
               .WithMany(player => player.Acceptions)
               .HasForeignKey(invitation => invitation.AcceptorId)
               .WillCascadeOnDelete(false);
       }
    }
}
