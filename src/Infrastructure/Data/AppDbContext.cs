using webapi.src.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace webapi.src.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration config;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
        {
            this.config = config;
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ModuleModel> Modules { get; set; }
        public DbSet<UserModuleSessionModel> UserModuleSessions { get; set; }
        public DbSet<SessionModel> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = config.GetConnectionString("Default");
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}