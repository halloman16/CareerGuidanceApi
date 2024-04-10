using webapi.src.Domain.Entities.Shared;
using webapi.src.Domain.Enums;

namespace webapi.src.Domain.Entities.Request
{
    public class TokenPayload
    {
        public TokenPair TokenPair { get; set; }
        public UserRole Role { get; set; }
    }
}