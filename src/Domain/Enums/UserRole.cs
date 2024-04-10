using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace webapi.src.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserRole
    {
        Common,
        Organization,
        Admin,
    }
}