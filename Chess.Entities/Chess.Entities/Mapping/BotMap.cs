﻿using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
    public class BotMap : EntityTypeConfiguration<Bot>
    {
        public BotMap()
        {
            HasKey(bot => bot.Id);
            Property(bot => bot.Id);
            Property(bot => bot.Level).IsRequired();
            Property(bot => bot.Name).IsRequired();

            ToTable("Bots");

            HasRequired(item => item.Player)
                .WithOptional(player => player.Bot)
                .WillCascadeOnDelete(false);
        }
    }
}