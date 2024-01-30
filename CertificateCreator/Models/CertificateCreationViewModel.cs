namespace CertificateCreator.Models
{
    public class CertificateCreationViewModel
    {
        public List<EmployeeDTO> employees { get; set; }
        public List<CertificateType> certificateTypes { get; set; }
        public List<EmployeesCertificatesWithDetails>  EmployeesCertificatesWithDetails { get; set; }
    }
}
