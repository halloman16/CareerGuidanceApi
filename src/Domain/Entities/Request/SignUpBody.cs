using System.ComponentModel.DataAnnotations;

namespace webapi.src.Domain.Entities.Request
{
    public class SignUpBody
    {
        [EmailAddress]
        [MaxLength(256)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}