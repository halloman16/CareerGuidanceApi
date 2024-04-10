using System.ComponentModel.DataAnnotations;

namespace webapi.src.Domain.Entities.Request
{
    public class CreateModuleBody
    {
        [Required]
        [StringLength(maximumLength: 512, MinimumLength = 2)]
        [RegularExpression(@"^(?![\s\p{P}]+$).+", ErrorMessage = "Строка не должна состоять из только пробелов или специальных символов.")]
        public string Name { get; set; }
    }
}