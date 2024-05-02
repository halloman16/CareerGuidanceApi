using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.Entities.Response;
using webapi.src.Domain.Models;

namespace webapi.src.Domain.IRepository
{
    public interface ISessionRepository
    {
        Task<SessionModel?> AddAsync(CreateSessionBody session, UserModel user, ModuleModel module);
        Task<IEnumerable<SessionModel>> GetAllByUserModuleSessionIdAsync(Guid userModuleSessionId);
        Task<SessionModel?> GetSessionByIdAsync(Guid sessionId);
        Task<bool> DeleteAsync(Guid sessionId);
        Task<UserModuleSessionModel?> GetAllSessions(Guid id);

        Task<SessionAnalyticsBody?> GetSessionAnalyticsBody(Guid id, int countTopSessions = 3);
    }
}