using System.ComponentModel.DataAnnotations;

namespace JiuJitsuWebApp.Models
{
    public class UserLoginRequests
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;


    }
}
