using Microsoft.EntityFrameworkCore;
using webapi.src.Domain.Entities.Request;
using webapi.src.Domain.Entities.Response;
using webapi.src.Domain.IRepository;
using webapi.src.Domain.Models;
using webapi.src.Infrastructure.Data;

namespace webapi.src.Infrastructure.Repository
{
    namespace webapi.src.Infrastructure.Repository
    {
        public class SessionRepository : ISessionRepository
        {
            private readonly AppDbContext _context;

            public SessionRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<SessionModel?> AddAsync(CreateSessionBody session, UserModel user, ModuleModel module)
            {
                var userModule = await _context.UserModuleSessions
                    .Include(e => e.Sessions)
                    .FirstOrDefaultAsync(e => e.UserId == user.Id && e.ModuleName == module.Name);

                if (userModule == null)
                {
                    userModule = new UserModuleSessionModel
                    {
                        Module = module,
                        User = user,
                    };

                    var userModuleSessionResult = await _context.UserModuleSessions.AddAsync(userModule);
                }

                var newSession = new SessionModel
                {
                    Duration = session.Duration,
                    IsSuccessful = session.IsSuccessful,
                    Mark = session.Mark,
                    MaxScore = session.MaxScore,
                    Score = session.Score,
                    UserModuleSession = userModule,
                };
                userModule.Sessions.Add(newSession);

                var sessionResult = await _context.Sessions.AddAsync(newSession);
                await _context.SaveChangesAsync();
                return sessionResult.Entity;
            }

            //public async Task<IEnumerable<SessionModel>> GetAllByUserModuleSessionIdAsync(Guid userModuleSessionId)
            //{
            //    return await _context.Sessions
            //        .Where(s => s.UserModuleSessionId == userModuleSessionId)
            //        .ToListAsync();
            //}

            public async Task<SessionModel?> GetSessionByIdAsync(Guid sessionId)
            {
                return await _context.Sessions
                    .FirstOrDefaultAsync(s => s.Id == sessionId);
            }

            public async Task<bool> DeleteAsync(Guid sessionId)
            {
                var session = await _context.Sessions.FindAsync(sessionId);
                if (session == null)
                    return false;

                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<List<UserModuleSessionModel>?> GetAllSessions(Guid id)
                => await _context.UserModuleSessions.Include(e => e.Sessions)
                    .Where(e => e.UserId == id).ToListAsync();

            //public async Task<SessionAnalyticsBody?> GetSessionAnalyticsBody(Guid id, int countTopSessions = 3)
            //{
            //    var userModule = await GetAllSessions(id);
            //    if (userModule == null)
            //        return null;

            //    var sessions = userModule.Sessions;
            //    var sessionsAnalyticsBody = new SessionAnalyticsBody
            //    {
            //        FailedSessions = sessions.Count(e => !e.IsSuccessful),
            //        SuccessfulSessions = sessions.Count(e => e.IsSuccessful),
            //        MaxScore = (sessions.MaxBy(e => e.Score)?.Score) ?? 0.0f,
            //        MinScore = (sessions.MinBy(e => e.Score)?.Score) ?? 0.0f,
            //        TotalSessions = sessions.Count,
            //        AverageScore = sessions.Average(e => e.Score),
            //        TopSessions = sessions.OrderByDescending(e => e.Score)
            //            .Take(countTopSessions)
            //            .Select(e =>
            //                e.ToSessionBody())
            //            .ToList()
            //    };

            //    return sessionsAnalyticsBody;
            //}
        }
    }

}