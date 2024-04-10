using webapi.src.Domain.Entities.Request;
using Microsoft.AspNetCore.Mvc;

namespace webapi.src.App.IService
{
    public interface IAuthService
    {
        Task<IActionResult> SignUp(SignUpBody body, string rolename);
        Task<IActionResult> SignIn(SignInBody body);
        Task<IActionResult> RestoreToken(string refreshToken);
        Task<IActionResult> Confirmation(ConfirmationBody body);
        Task<IActionResult> ResetPassword(PasswordResetBody body);
    }
}