using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.Entities.Response;
using webapi.src.Domain.IRepository;
using webApiTemplate.src.App.IService;

namespace webapi.src.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class SessionController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IModuleRepository _moduleRepository;

        private readonly IJwtService _jwtService;

        public SessionController(
            IUserRepository userRepository,
            IModuleRepository moduleRepository,
            ISessionRepository sessionRepository,
            IJwtService jwtService
        )
        {
            _userRepository = userRepository;
            _moduleRepository = moduleRepository;
            _sessionRepository = sessionRepository;
            _jwtService = jwtService;
        }

        [HttpPost("session/{moduleName}"), Authorize]
        [SwaggerOperation("Создать новую сессию")]
        [SwaggerResponse(200, Description = "Успешно", Type = typeof(SessionBody), ContentTypes = new string[] { MediaTypeNames.Application.Json })]
        [SwaggerResponse(400, Description = "Неверные значения диапазона для количества очков")]
        [SwaggerResponse(404, Description = "Модуль не найден")]
        public async Task<IActionResult> CreateSession(
            string moduleName,
            CreateSessionBody createSessionBody,
            [FromHeader(Name = "Authorization")] string token
            )
        {
            var module = await _moduleRepository.GetModuleByNameAsync(moduleName);
            if (module == null)
                return NotFound();

            if (createSessionBody.Score > createSessionBody.MaxScore ||
                createSessionBody.Score < 0 ||
                createSessionBody.MaxScore < 0
            )
                return BadRequest();

            var userId = _jwtService.GetUserId(token);
            var user = await _userRepository.GetAsync(userId);
            var result = await _sessionRepository.AddAsync(createSessionBody, user, module);
            return result == null ? BadRequest() : Ok(result.ToSessionBody());
        }


        [HttpGet("session/{sessionId}"), Authorize]
        [SwaggerOperation("Получить сессию по ID")]
        [SwaggerResponse(200, Description = "Успешно", Type = typeof(SessionBody), ContentTypes = new string[] { MediaTypeNames.Application.Json })]
        [SwaggerResponse(404, Description = "Сессия не найдена")]
        public async Task<IActionResult> GetSession(Guid sessionId)
        {
            var session = await _sessionRepository.GetSessionByIdAsync(sessionId);
            return session == null ? NotFound() : Ok(session.ToSessionBody());
        }

        [HttpGet("sessions/{moduleName}"), Authorize]
        [SwaggerOperation("Получить все сессии пользователя в модуле")]
        [SwaggerResponse(200, Description = "Успешно", Type = typeof(IEnumerable<SessionBody>), ContentTypes = new string[] { MediaTypeNames.Application.Json })]
        public async Task<IActionResult> GetSessionsByUserModuleSessionId(
            [FromHeader(Name = "Authorization")] string token,
            string moduleName)
        {
            var userId = _jwtService.GetUserId(token);
            var userModule = await _sessionRepository.GetAllSessions(userId);
            return Ok(userModule?.Sessions.Select(e => e.ToSessionBody()) ?? new List<SessionBody>());
        }

        // [HttpGet("sessions/analytics/{moduleName}")]
        // [SwaggerOperation("Получить аналитику пользователя по модулю")]
        // [SwaggerResponse(200, Description = "Успешно", Type = typeof(SessionAnalyticsBody), ContentTypes = new string[] { MediaTypeNames.Application.Json })]
        // [SwaggerResponse(404, Description = "Модуль не существует")]

        // public async Task<IActionResult> GetAnalytics(
        //     [FromHeader(Name = "Authorization")] string token,
        //     string moduleName
        // )
        // {
        //     var userId = _jwtService.GetUserId(token);
        //     var analyticsBody = await _sessionRepository.GetSessionAnalyticsBody(moduleName, userId) ??
        //     new SessionAnalyticsBody
        //     {
        //         AverageScore = 0,
        //         FailedSessions = 0,
        //         MaxScore = 0,
        //         MinScore = 0,
        //         SuccessfulSessions = 0,
        //         TotalSessions = 0,
        //         TopSessions = new List<SessionBody>(),
        //     };
        //     return Ok(analyticsBody);
        // }
    }
}