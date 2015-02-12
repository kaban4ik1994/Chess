using System.ComponentModel.DataAnnotations;

namespace Chess.Models
{
    public class LogInViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}