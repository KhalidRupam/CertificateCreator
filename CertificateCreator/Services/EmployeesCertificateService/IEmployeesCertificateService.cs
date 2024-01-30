using CertificateCreator.Models;

namespace CertificateCreator.Services.EmployeesCertificateService
{
    public interface IEmployeesCertificateService
    {
        Task<string> insertEmployeesCertificate(EmployeesCertificate employeesCertificate);
        Task<string> updateEmployeesCertificate(EmployeesCertificate employeesCertificate);
        Task<List<EmployeesCertificatesWithDetails>> GetAllEmployeesCertificates();
        Task<string> DeleteEmployeesCertificates(int id);
        Task<List<EmployeesCertificatesWithDetails>> GetAllEmployeesCertificatesByEmployeeId(string creatorId);
    }
}
