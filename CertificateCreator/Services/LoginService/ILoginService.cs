using CertificateCreator.Models;

namespace CertificateCreator.Services.LoginService
{
    public interface ILoginService
    {
        Task<string> checkLogin(LoginEntity user);
        Task<LoginEntity> insertUser(LoginEntity user);
        Task<string> verifyOTP(string Email, string OTP);
    }
}
