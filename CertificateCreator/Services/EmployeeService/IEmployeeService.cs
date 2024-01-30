using CertificateCreator.Models;

namespace CertificateCreator.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployeeDetails();
    }
}
