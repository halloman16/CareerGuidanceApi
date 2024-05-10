using webapi.src.Domain.IRepository;
using webapi.src.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webApiTemplate.src.App.IService;
using System.Net.Mime;

namespace webapi.src.Web.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly ISessionRepository _sessionRepository;

        private readonly IJwtService _jwtService;

        public UploadController(
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

        [HttpGet("module/{filename}")]
        [SwaggerOperation("Получить архив модуля")]
        [SwaggerResponse(200, Description = "Успешно", Type = typeof(File), ContentTypes = new string[] { "application/zip" })]
        [SwaggerResponse(404, Description = "Файла с таким именем нет")]

        public async Task<IActionResult> GetModuleZipFileAsync(string filename)
        {
            var bytes = await FileUploader.GetStreamFileAsync(Constants.localPathToModuleFiles, filename);
            if (bytes == null)
                return NotFound();

            return File(bytes, "application/zip");
        }

        [HttpPost("module/{moduleName}"), Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(1024 * 1024 * 1024)]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = 1024 * 1024 * 1024)]
        [SwaggerOperation("Загрузить архив модуля как form-data (администратор)")]
        [SwaggerResponse(200, Description = "Успешно загружен", Type = typeof(string))]
        [SwaggerResponse(400, Description = "Файлы не прикреплены")]
        [SwaggerResponse(404, Description = "Неверное имя модуля")]
        [SwaggerResponse(415, Description = "Поддерживаемые типы: zip, rar, 7z")]
        public async Task<IActionResult> UploadZipFileToModule(string moduleName)
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return BadRequest();

            var fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension == null || (fileExtension != ".zip" && fileExtension != ".rar" && fileExtension != ".7z"))
                return new UnsupportedMediaTypeResult();

            var module = await _moduleRepository.GetModuleByNameAsync(moduleName);
            if (module == null)
                return NotFound();


            var filename = await FileUploader.UploadFileAsync(Constants.localPathToModuleFiles, file.OpenReadStream(), fileExtension);
            var result = _moduleRepository.UpdateFileAsync(moduleName, filename);
            return result == null ? NotFound() : Ok(filename);
        }

        [HttpPost("deleteModule"), Authorize(Roles = "Admin")]
        [SwaggerOperation("Удалить архив модуля (администратор)")]
        [SwaggerResponse(200, Description = "Успешно удалён", Type = typeof(string))]
        [SwaggerResponse(404, Description = "Неверное имя модуля")]
        public async Task<IActionResult> DeleteModuleZipFileAsync(string fileUrl)
        {
            string filename = fileUrl.Replace(Constants.webPathToModuleFile, "");

            Console.WriteLine(Path.Combine(Constants.localPathToModuleFiles, filename));

            var bytes = await FileUploader.GetStreamFileAsync(Constants.localPathToModuleFiles, filename);
            if (bytes == null)
                return NotFound();

            System.IO.File.Delete(Path.Combine(Constants.localPathToModuleFiles, filename));

            return Ok();
        }
    }
}