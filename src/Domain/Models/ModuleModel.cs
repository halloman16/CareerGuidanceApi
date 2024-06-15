using System.ComponentModel.DataAnnotations;
using webapi.src.Domain.Entities.Response;

namespace webapi.src.Domain.Models
{
    public class ModuleModel
    {
        [Key]
        public string Name { get; set; }
        public string? FileName { get; set; }

        public List<UserModuleSessionModel> UserModuleSessions { get; set; }

        public ModuleBody ToModuleBody()
        {
            return new ModuleBody
            {
                Name = Name,
                UrlFile = FileName == null ? null : $"{Constants.webPathToModuleFile}{FileName}"
            };
        }
    }
}