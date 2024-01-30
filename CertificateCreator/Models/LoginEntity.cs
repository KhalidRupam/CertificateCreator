namespace CertificateCreator.Models
{
    public class LoginEntity
    {
        public string Id { get; set; } = "N/A";
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
