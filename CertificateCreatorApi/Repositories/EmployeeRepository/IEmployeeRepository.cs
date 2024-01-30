using CertificateCreatorApi.Models;

namespace CertificateCreatorApi.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDTO>> GetAllEmployeeDetails();
    }
}
