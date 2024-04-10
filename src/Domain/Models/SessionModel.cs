using webapi.src.Domain.Entities.Response;

namespace webapi.src.Domain.Models
{
    public class SessionModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public TimeSpan Duration { get; set; }
        public float Score { get; set; }
        public float MaxScore { get; set; }
        public bool IsSuccessful { get; set; }
        public float Mark { get; set; }
        public string? DescriptionEvaluationReason { get; set; }
        public string? RecordingFilename { get; set; }
        public Guid UserModuleSessionId { get; set; }
        public UserModuleSessionModel UserModuleSession { get; set; }

        public SessionBody ToSessionBody()
        {
            return new SessionBody
            {
                Date = Date,
                DescriptionEvaluationReason = DescriptionEvaluationReason,
                Duration = Duration,
                Id = Id,
                IsSuccessful = IsSuccessful,
                Mark = Mark,
                MaxScore = MaxScore,
                Score = Score,
                UrlRecordingFile = RecordingFilename == null ? null : $"{Constants.webPathToSessionRecordingFile}{RecordingFilename}"
            };
        }
    }
}