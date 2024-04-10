using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webapi.src.Domain.Entities.Request
{
    public class CreateSessionBody
    {
        public TimeSpan Duration { get; set; }
        [Range(0.0, float.MaxValue)]
        [DefaultValue(0.0f)]
        public float Score { get; set; }

        [Range(0.0, float.MaxValue)]
        [DefaultValue(100.0f)]
        public float MaxScore { get; set; }
        public bool IsSuccessful { get; set; }
        public float Mark { get; set; }
        public string? DescriptionEvaluationReason { get; set; }
    }
}