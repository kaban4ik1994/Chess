using System.ComponentModel.DataAnnotations;
using Chess.Enums;

namespace Chess.WebAPI.Models
{
    public class AddInvitationWithBotViewModel
    {
        [Required]
        public long InvitatorId { get; set; }

        [Required]
        public BotType BotType { get; set; }
    }
}