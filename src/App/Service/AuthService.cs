using webapi.src.App.IService;
using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using webApiTemplate.src.App.IService;
using webapi.src.Domain.Enums;

namespace webapi.src.App.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        private readonly TimeSpan _duration;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService
        )
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _duration = new TimeSpan(1, 0, 0, 0);
        }

        //public async Task<IActionResult> ResetPassword(PasswordResetBody body)
        //{
        //    var user = await _userRepository.GetAsync(body.Email);
        //    if (user == null)
        //        return new NotFoundResult();

        //    if (body.Email == "testy@gmail.com" && body.RecoveryCode == "111111")
        //    {
        //        await _userRepository.ResetPassword(user, user.PasswordHash);
        //        var testTokenPair = _jwtService.GenerateDefaultTokenPair(user.Id, user.RoleName);
        //        var testTokenPayload = new TokenPayload
        //        {
        //            TokenPair = testTokenPair,
        //            Role = Enum.Parse<UserRole>(user.RoleName),
        //        };

        //        var testRefreshToken = await _userRepository.UpdateTokenAsync(testTokenPair.RefreshToken, user.Id, _duration);
        //        testTokenPayload.TokenPair.RefreshToken = testRefreshToken;

        //        return new OkObjectResult(testTokenPayload);
        //    }
        //    else if (body.Email == "testy@gmail.com" && body.RecoveryCode != "111111")
        //        return new BadRequestResult();

        //    if (!user.WasPasswordResetRequest || body.RecoveryCode != user.RecoveryCode || user.RecoveryCodeValidBefore < DateTime.UtcNow)
        //        return new BadRequestResult();
        //    await _userRepository.ResetPassword(user, body.Password);

        //    var tokenPair = _jwtService.GenerateDefaultTokenPair(user.Id, user.RoleName);
        //    var tokenPayload = new TokenPayload
        //    {
        //        TokenPair = tokenPair,
        //        Role = Enum.Parse<UserRole>(user.RoleName),
        //    };

        //    var refreshToken = await _userRepository.UpdateTokenAsync(tokenPair.RefreshToken, user.Id, _duration);
        //    tokenPayload.TokenPair.RefreshToken = refreshToken;

        //    return new OkObjectResult(tokenPayload);
        //}

        //public async Task<IActionResult> Confirmation(ConfirmationBody body)
        //{
        //    var user = await _userRepository.GetAsync(body.Email);
        //    if (user == null)
        //        return new NotFoundResult();

        //    if (body.Email == "testy@gmail.com" && body.RecoveryCode == "111111")
        //        return new OkResult();
        //    else if (body.Email == "testy@gmail.com" && body.RecoveryCode != "111111")
        //        return new BadRequestResult();

        //    if (!user.WasPasswordResetRequest || body.RecoveryCode != user.RecoveryCode || user.RecoveryCodeValidBefore < DateTime.UtcNow)
        //        return new BadRequestResult();
        //    return new OkResult();
        //}

        public async Task<IActionResult> RestoreToken(string refreshToken)
        {
            var oldUser = await _userRepository.GetByTokenAsync(refreshToken);
            if (oldUser == null)
                return new NotFoundResult();

            var tokenPair = _jwtService.GenerateDefaultTokenPair(oldUser.Id, oldUser.RoleName);
            var tokenPayload = new TokenPayload
            {
                TokenPair = tokenPair,
                Role = Enum.Parse<UserRole>(oldUser.RoleName)!,
            };
            var token = await _userRepository.UpdateTokenAsync(tokenPair.RefreshToken, oldUser.Id, _duration);

            tokenPayload.TokenPair.RefreshToken = token;
            return new OkObjectResult(tokenPayload);
        }

        public async Task<IActionResult> SignIn(SignInBody body)
        {
            var oldUser = await _userRepository.GetAsync(body.Email);
            if (oldUser == null)
                return new NotFoundResult();

            if (oldUser.PasswordHash != body.Password)
                return new BadRequestResult();

            var tokenPair = _jwtService.GenerateDefaultTokenPair(oldUser.Id, oldUser.RoleName);
            var tokenPayload = new TokenPayload
            {
                TokenPair = tokenPair,
                Role = Enum.Parse<UserRole>(oldUser.RoleName)!,
            };
            var token = await _userRepository.UpdateTokenAsync(tokenPair.RefreshToken, oldUser.Id, _duration);

            tokenPayload.TokenPair.RefreshToken = token;

            return new OkObjectResult(tokenPayload);
        }

        public async Task<IActionResult> SignUp(SignUpBody body, string rolename)
        {
            var oldUser = await _userRepository.AddAsync(body, rolename);
            if (oldUser == null)
                return new ConflictResult();

            var tokenPair = _jwtService.GenerateDefaultTokenPair(oldUser.Id, oldUser.RoleName);
            var tokenPayload = new TokenPayload
            {
                TokenPair = tokenPair,
                Role = Enum.Parse<UserRole>(oldUser.RoleName)!,
            };
            var token = await _userRepository.UpdateTokenAsync(tokenPair.RefreshToken, oldUser.Id, _duration);

            tokenPayload.TokenPair.RefreshToken = token;
            return new OkObjectResult(tokenPayload);
        }
    }
}