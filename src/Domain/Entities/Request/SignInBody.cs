using System.ComponentModel.DataAnnotations;

namespace webapi.src.Domain.Entities.Request
{
    public class SignInBody
    {
        [EmailAddress]
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}