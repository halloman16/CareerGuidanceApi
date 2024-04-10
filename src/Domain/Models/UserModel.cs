using System.ComponentModel.DataAnnotations;
using webapi.src.Domain.Entities.Response;
using webapi.src.Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace webapi.src.Domain.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Token), IsUnique = true)]
    public class UserModel
    {
        public Guid Id { get; set; }

        [StringLength(256, MinimumLength = 3)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? RestoreCode { get; set; }
        public string RoleName { get; set; }
        public DateTime? RestoreCodeValidBefore { get; set; }
        public DateTime? RecoveryCodeValidBefore { get; set; }
        public string? RecoveryCode { get; set; }
        public bool WasPasswordResetRequest { get; set; }
        public string? Token { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? TokenValidBefore { get; set; }

        public List<ModuleModel> CreatedModules { get; set; }
        public List<UserModuleSessionModel> UserModuleSessions { get; set; }

        public ProfileBody ToProfileBody()
        {
            return new ProfileBody
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Role = Enum.Parse<UserRole>(RoleName),
                UrlIcon = string.IsNullOrEmpty(Image) ? null : $"{Constants.webPathToProfileIcons}{Image}",
            };
        }
    }
}