using webapi.src.Domain.Entities.Response;
using webapi.src.Domain.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webApiTemplate.src.App.IService;
using System.Net.Mime;

namespace webapi.src.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public ProfileController(
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }


        [HttpGet("profile"), Authorize]
        [SwaggerOperation("Получить профиль")]
        [SwaggerResponse(200, Description = "Успешно", Type = typeof(ProfileBody), ContentTypes = new string[] { MediaTypeNames.Application.Json })]
        public async Task<IActionResult> GetProfileAsync([FromHeader(Name = "Authorization")] string token)
        {
            var userId = _jwtService.GetUserId(token);
            if (userId == Guid.Empty)
                return Unauthorized();

            var user = await _userRepository.GetAsync(userId);
            return user == null ? NotFound() : Ok(user.ToProfileBody());
        }
    }
}