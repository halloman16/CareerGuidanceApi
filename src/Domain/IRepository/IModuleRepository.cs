using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.Models;

namespace webapi.src.Domain.IRepository
{
    public interface IModuleRepository
    {
        Task<IEnumerable<ModuleModel>> GetAllModulesAsync();
        Task<ModuleModel?> GetModuleByNameAsync(string name);
        Task<ModuleModel?> AddAsync(CreateModuleBody module, UserModel model);
        Task<ModuleModel?> UpdateFileAsync(string name, string filename);
    }
}