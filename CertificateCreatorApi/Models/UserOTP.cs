namespace CertificateCreatorApi.Models
{
    public class UserOTP
    {
        public int Id { get; set; }
        public string OTP { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
