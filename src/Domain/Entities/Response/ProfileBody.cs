using webapi.src.Domain.Enums;

namespace webapi.src.Domain.Entities.Response
{
    public class ProfileBody
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string? UrlIcon { get; set; }
    }
}