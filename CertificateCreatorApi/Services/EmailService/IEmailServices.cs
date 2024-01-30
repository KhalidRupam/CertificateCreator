namespace CertificateCreatorApi.Services.EmailService
{
    public interface IEmailServices
    {
        Task<string> SendMail(string email, string subject, string body);
    }
}
