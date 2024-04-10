using System.ComponentModel.DataAnnotations;

namespace webapi.src.Domain.Entities.Request
{
    public class EmailBody
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}