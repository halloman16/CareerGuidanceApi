using webapi.src.App.IService;
using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using webapi.src.Domain.IRepository;
using System.Net;


namespace webapi.src.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        private readonly string testEmail = "testy@gmail.com";


        public AuthController(
            IAuthService authService,
            IEmailService emailService,
            IUserRepository userRepository
        )
        {
            _authService = authService;
            _emailService = emailService;
            _userRepository = userRepository;
        }


        [SwaggerOperation("Регистрация")]
        [SwaggerResponse(200, "Успешно создан", Type = typeof(TokenPayload), ContentTypes = new string[] { MediaTypeNames.Application.Json })]
        [SwaggerResponse(409, "Почта уже существует")]

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync(SignUpBody signUpBody)
        {
            string role = Enum.GetName(UserRole.Common)!;
            var result = await _authService.SignUp(signUpBody, role);
            return result;
        }



        [SwaggerOperation("Авторизация")]
        [SwaggerResponse(200, "Успешно", Type = typeof(TokenPayload), ContentTypes = new string[] { MediaTypeNames.Application.Json })]
        [SwaggerResponse(400, "Пароли не совпадают")]
        [SwaggerResponse(404, "Email не зарегистрирован")]

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync(SignInBody signInBody)
        {
            var result = await _authService.SignIn(signInBody);
            return result;
        }

        //[HttpPost("confirmation")]
        //[SwaggerOperation(Summary = "Подтвердить код восстановления аккаунта")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Description = "Успешно")]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Ошибочный код или истекло время действия")]
        //[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Пользователь не существует")]

        //public async Task<IActionResult> RecoveryCodeConfirmation(ConfirmationBody body)
        //{
        //    var result = await _authService.Confirmation(body);
        //    return result;
        //}

        //[HttpPost("reset")]
        //[SwaggerOperation(Summary = "Сброс пароля")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Description = "Пароль успешно сброшен", Type = typeof(TokenPayload))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Ошибочный код или истекло время действия")]
        //[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Пользователь не существует")]

        //public async Task<IActionResult> ResetPassword(PasswordResetBody body)
        //{
        //    var response = await _authService.ResetPassword(body);
        //    if (response is OkObjectResult okObjectResult)
        //    {
        //        var tokenPayload = (TokenPayload)okObjectResult.Value;
        //        if (body.Email != testEmail)
        //            await _emailService.SendEmail(body.Email, "Пароль успешно изменен", "");
        //        return Ok(tokenPayload);
        //    }
        //    return response;
        //}

        //[HttpPost("recovery")]
        //[SwaggerOperation(Summary = "Отправить код восстановления на почту")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Description = "Успешно")]
        //[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Пользователь не существует")]

        //public async Task<IActionResult> SendRecoveryCode(EmailBody body)
        //{
        //    var recoveryCode = await _userRepository.GenerateRecoveryCode(body.Email);
        //    if (recoveryCode == null)
        //        return NotFound();

        //    if (body.Email == testEmail)
        //        return Ok();

        //    await _emailService.SendEmail(
        //        email: body.Email,
        //        subject: "Код восстановления пароля",
        //        message: $"Код восстановления: {recoveryCode}. " +
        //        $"Если это были не вы, проигнорируйте данное сообщение");
        //    return Ok();
        //}

        //[SwaggerOperation("Восстановление токена")]
        //[SwaggerResponse(200, "Успешно создан", Type = typeof(TokenPayload))]
        //[SwaggerResponse(404, "Токен не используется")]

        //[HttpPost("token")]
        //public async Task<IActionResult> RestoreTokenAsync(TokenBody body)
        //{
        //    var result = await _authService.RestoreToken(body.Token);
        //    return result;
        //}
    }
}