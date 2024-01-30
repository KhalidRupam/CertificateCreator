using CertificateCreator.Models;
using System.Text;

namespace CertificateCreator.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private HttpClient _httpClient;

        private static string ApiUsers => $"Login";

        public LoginService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HttpClient");
        }

        public async Task<LoginEntity> insertUser(LoginEntity user)
        {
            try
            {
                var responseMessage = await _httpClient.PostAsJsonAsync(ApiUsers, user);
                return user;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> verifyOTP(string Email, string OTP)
        {
            try
            {
                var url = ApiUsers + "/UpdateOTP";

                OTPEntity oTPEntity = new OTPEntity() { Email = Email, OTP = OTP };


                var responseMessage = await _httpClient.PatchAsync(url, JsonContent.Create(oTPEntity));
                return responseMessage.StatusCode == System.Net.HttpStatusCode.OK ? "OK" : "OTP is not matched";

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> checkLogin(LoginEntity user)
        {
            try
            {
                var url = ApiUsers + "/CheckLogin";
                var responseMessage = await _httpClient.PostAsJsonAsync(url, user);

                var res = await responseMessage.Content.ReadAsStringAsync();
                return res;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
