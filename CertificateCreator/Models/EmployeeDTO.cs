namespace CertificateCreator.Models
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public DateTime CreationDate { get; set; }
        public string DepartmentName { get; set; }
    }
}
