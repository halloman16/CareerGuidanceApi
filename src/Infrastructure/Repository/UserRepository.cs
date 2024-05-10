using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.IRepository;
using webapi.src.Domain.Models;
using webapi.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using webapi.src.Utility;

namespace webapi.src.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel?> AddAsync(SignUpBody body, string role)
        {
            var oldUser = await GetAsync(body.Email);
            if (oldUser != null)
                return null;

            var newUser = new UserModel
            {
                FirstName = body.FirstName,
                LastName = body.LastName,
                Email = body.Email,
                PasswordHash = body.Password,
                RoleName = role,
            };

            var result = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return result?.Entity;
        }

        public async Task<UserModel?> GetAsync(Guid id)
            => await _context.Users
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<UserModel?> GetAsync(string email)
            => await _context.Users
                .FirstOrDefaultAsync(e => e.Email == email);

        public async Task<UserModel?> GetByTokenAsync(string refreshTokenHash)
            => await _context.Users
            .FirstOrDefaultAsync(e => e.Token == refreshTokenHash);

        public async Task<UserModel?> GetUserModulesSessions(Guid id)
            => await _context.Users
                .Include(e => e.UserModuleSessions)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<string?> GenerateRecoveryCode(string email, TimeSpan? interval = null)
        {
            interval ??= TimeSpan.FromMinutes(10.0);
            var user = await GetAsync(email);
            if (user == null)
                return null;

            user.RecoveryCode = RecoveryCodeGenerator.Generate();
            user.RecoveryCodeValidBefore = DateTime.UtcNow.Add(interval.Value);
            user.WasPasswordResetRequest = true;
            await _context.SaveChangesAsync();

            return user.RecoveryCode;
        }

        public async Task<UserModel> ResetPassword(UserModel user, string newPassword)
        {
            user.PasswordHash = newPassword;
            user.WasPasswordResetRequest = false;
            user.RecoveryCodeValidBefore = null;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<string?> UpdateTokenAsync(string newRefreshToken, Guid id, TimeSpan duration)
        {
            var user = await GetAsync(id);
            if (user == null)
                return null;

            var currentDate = DateTime.UtcNow;
            if (user.TokenValidBefore == null)
            {
                user.TokenValidBefore = currentDate.Add(duration);
                user.Token = newRefreshToken;
                await _context.SaveChangesAsync();
                return newRefreshToken;
            }

            else if (user.TokenValidBefore < currentDate)
            {
                user.TokenValidBefore = currentDate.Add(duration);
                user.Token = newRefreshToken;
                await _context.SaveChangesAsync();
                return newRefreshToken;
            }

            return user.Token;
        }
    }
}