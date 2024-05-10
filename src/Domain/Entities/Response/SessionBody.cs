using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webapi.src.Domain.Entities.Response
{
    public class SessionBody
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string ModuleName { get; set; }
        [DataType(DataType.Duration)]
        public TimeSpan Duration { get; set; }
        public float Score { get; set; }
        public float MaxScore { get; set; }
        public bool IsSuccessful { get; set; }
        public float Mark { get; set; }
        public string? DescriptionEvaluationReason { get; set; }
    }
}