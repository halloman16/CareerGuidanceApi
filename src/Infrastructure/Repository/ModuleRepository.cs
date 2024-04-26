using Microsoft.EntityFrameworkCore;
using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.IRepository;
using webapi.src.Domain.Models;
using webapi.src.Infrastructure.Data;

namespace webapi.src.Infrastructure.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly AppDbContext _context;

        public ModuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ModuleModel?> AddAsync(CreateModuleBody newModule, UserModel creator)
        {
            var module = await GetModuleByNameAsync(newModule.Name);
            if (module != null)
                return null;

            var moduleModel = new ModuleModel
            {
                Creator = creator,
                Name = newModule.Name
            };

            var result = await _context.Modules.AddAsync(moduleModel);
            await _context.SaveChangesAsync();
            return result?.Entity;
        }

        public async Task<bool> Delete(string moduleName)
        {
            var module = await GetModuleByNameAsync(moduleName);
            if (module == null)
                return false;

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ModuleModel>> GetAllModulesAsync()
            => await _context.Modules.ToListAsync();

        public async Task<ModuleModel?> GetModuleByNameAsync(string name)
            => await _context.Modules
                .FirstOrDefaultAsync(e => e.Name == name);

        public async Task<ModuleModel?> UpdateFileAsync(string name, string filename)
        {
            var module = await GetModuleByNameAsync(name);
            if (module == null)
                return null;

            module.FileName = filename;
            await _context.SaveChangesAsync();
            return module;
        }
    }
}