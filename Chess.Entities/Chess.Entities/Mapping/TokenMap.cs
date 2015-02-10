using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
    public class TokenMap : EntityTypeConfiguration<Token>
    {
        public TokenMap()
        {
            HasKey(item => item.TokenId);

            Property(item => item.TokenId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.TokenData).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("Tokens");

            HasRequired(item => item.User).WithMany(data => data.Tokens).HasForeignKey(item => item.UserId);
        }
    }
}
