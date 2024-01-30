using CertificateCreatorApi.Models;

namespace CertificateCreatorApi.Repositories.LoginRepository
{
    public interface ILoginRepository
    {
        Task<string> CreateUser(LoginEntity loginEntity);
        Task<int> CreateOtp(UserOTP otp);
        Task<LoginDetailsWithOTP> GetLoginByEmailId(string EmailId);
        Task<int> UpdateUser(LoginEntity loginEntity);
    }
}
