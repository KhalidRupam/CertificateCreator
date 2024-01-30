namespace CertificateCreator.Models
{
    public class EmployeesCertificate
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CertificateId { get; set; }
        public string PDFUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
