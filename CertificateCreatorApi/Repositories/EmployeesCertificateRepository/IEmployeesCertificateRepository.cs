using CertificateCreatorApi.Models;

namespace CertificateCreatorApi.Repositories.EmployeesCertificateRepository
{
    public interface IEmployeesCertificateRepository
    {
        Task<int> CreateEmployeesCertificate(EmployeesCertificate employeesCertificate);
        Task<List<EmployeesCertificatesWithDetails>> GetAllEmployeesCertificates();
        Task<int> DeleteEmployeesCertificates(int id);
    }
}
