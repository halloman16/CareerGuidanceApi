namespace webapi.src.Domain.Entities.Request
{
    public class PasswordResetBody
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RecoveryCode { get; set; }
    }
}