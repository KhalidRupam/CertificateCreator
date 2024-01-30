using System.Net.Mail;
using System.Net;

namespace CertificateCreatorApi.Services.EmailService
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration config;
        private readonly string senderEmail;
        private readonly string senderPassword;
        private readonly string smtpServer;
        private readonly int smtpPort;

        public EmailServices(IConfiguration config)
        {
            senderEmail = config["EmailSettings:senderEmail"];
            senderPassword = config["EmailSettings:senderPassword"];
            smtpServer = config["EmailSettings:smtpServer"];
            smtpPort = int.Parse(config["EmailSettings:smtpPort"]);
        }
        public async Task<string> SendMail(string email, string subject, string body)
        {
           
            try
            {
                string recipientEmail = email;

                MailMessage mail = new MailMessage(senderEmail, recipientEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };
                smtpClient.Send(mail);
                return "Email sent successfully!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return ex.Message;
            }
        }
    }
}
