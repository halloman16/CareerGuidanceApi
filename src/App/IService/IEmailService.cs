namespace webapi.src.App.IService
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}