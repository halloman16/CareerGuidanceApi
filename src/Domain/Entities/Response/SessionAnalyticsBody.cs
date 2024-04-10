namespace webapi.src.Domain.Entities.Response
{
    public class SessionAnalyticsBody
    {
        public int TotalSessions { get; set; }
        public int SuccessfulSessions { get; set; }
        public int FailedSessions { get; set; }
        public float AverageScore { get; set; }
        public float MinScore { get; set; }
        public float MaxScore { get; set; }
        public List<SessionBody> TopSessions { get; set; } = new();
    }
}