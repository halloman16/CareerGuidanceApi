namespace webapi.src.Domain.Models
{
    public class UserModuleSessionModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public string ModuleName { get; set; }
        public ModuleModel Module { get; set; }
        public List<SessionModel> Sessions { get; set; } = new();
    }
}