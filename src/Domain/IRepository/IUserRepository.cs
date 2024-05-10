using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.Models;

namespace webapi.src.Domain.IRepository
{
    public interface IUserRepository
    {
        Task<UserModel?> AddAsync(SignUpBody body, string role);
        Task<UserModel?> GetAsync(Guid id);
        Task<UserModel?> GetAsync(string email);
        Task<UserModel?> GetUserModulesSessions(Guid id);
        Task<string?> UpdateTokenAsync(string newRefreshToken, Guid id, TimeSpan duration);
        Task<UserModel?> GetByTokenAsync(string refreshTokenHash);
        Task<UserModel> ResetPassword(UserModel user, string newPassword);
        Task<string?> GenerateRecoveryCode(string email, TimeSpan? interval = null);
    }
}