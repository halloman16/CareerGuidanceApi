namespace webapi.src.Domain.Entities.Request
{
    public class ConfirmationBody
    {
        public string Email { get; set; }
        public string RecoveryCode { get; set; }
    }
}