using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.Entities.Response;
using webapi.src.Domain.IRepository;
using webapi.src.Domain.Models;
using webApiTemplate.src.App.IService;

namespace webapi.src.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public ModuleController(
            IModuleRepository moduleRepository,
            IUserRepository userRepository,
            IJwtService jwtService
        )
        {
            _moduleRepository = moduleRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpGet("modules"), Authorize]
        [SwaggerOperation(Summary = "Получить все модули", Description = "Получить список всех модулей.")]
        [SwaggerResponse(200, Description = "Список получен", Type = typeof(IEnumerable<ModuleBody>), ContentTypes = new string[] { MediaTypeNames.Application.Json })]
        public async Task<IActionResult> GetModules()
        {
            var modules = await _moduleRepository.GetAllModulesAsync();
            return Ok(modules.Select(e => e.ToModuleBody()));
        }

        [HttpGet("module/{name}"), Authorize]
        [SwaggerOperation(Summary = "Получить модуль по имени", Description = "Получить информацию о модуле по его имени.")]
        [SwaggerResponse(200, "Модуль найден", typeof(ModuleBody), ContentTypes = new string[] { MediaTypeNames.Application.Json })]
        [SwaggerResponse(404, "Модуль не найден")]
        public async Task<ActionResult<ModuleModel>> GetModule(string name)
        {
            var module = await _moduleRepository.GetModuleByNameAsync(name);
            if (module == null)
                return NotFound();
            return Ok(module.ToModuleBody());
        }

        [HttpPost("module"), Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Создать новый модуль (создает аккаунт, являющийся администратором)", Description = "Создать новый модуль.")]
        [SwaggerResponse(201, "Модуль успешно создан", typeof(ModuleBody))]
        [SwaggerResponse(409, "Модуль с таким именем уже существует")]
        public async Task<ActionResult<ModuleModel>> CreateModule(CreateModuleBody newModule, [FromHeader(Name = "Authorization")] string token)
        {
            var creatorId = _jwtService.GetUserId(token);
            var creator = await _userRepository.GetAsync(creatorId);

            var createdModule = await _moduleRepository.AddAsync(newModule, creator!);
            if (createdModule == null)
                return Conflict("Модуль с таким именем уже существует.");

            return CreatedAtAction(nameof(GetModule), new { name = createdModule.Name }, createdModule.ToModuleBody());
        }

        [HttpPost("deleteModule"), Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Удалить модуль (удаляет аккаунт, являющийся администратором)", Description = "Удалить модуль.")]
        [SwaggerResponse(201, "Модуль успешно удалён", typeof(ModuleBody))]
        [SwaggerResponse(409, "Модуль с таким именем уже существует")]
        public async Task<ActionResult> DeleteModule(string moduleName, [FromHeader(Name = "Authorization")] string token)
        {
            var createdModule = await _moduleRepository.Delete(moduleName);
            if (!createdModule)
                return Conflict("Такого модуля не существует");

            return CreatedAtAction(nameof(DeleteModule), "Модуль успешно удалён");
        }
    }
}