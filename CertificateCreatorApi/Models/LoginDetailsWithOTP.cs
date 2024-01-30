namespace CertificateCreatorApi.Models
{
    public class LoginDetailsWithOTP
    {
        public LoginEntity loginEntity { get; set; }
        public UserOTP userOTP { get; set; }
        public string token { get; set; }
    }
}
