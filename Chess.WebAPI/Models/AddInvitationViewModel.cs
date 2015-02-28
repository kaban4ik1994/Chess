using System.ComponentModel.DataAnnotations;

namespace Chess.WebAPI.Models
{
    public class AddInvitationViewModel
    {
        [Required]
        public long InvitatorId { get; set; }
    }
}