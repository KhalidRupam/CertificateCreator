using CertificateCreator.Models;

namespace CertificateCreator.Services.CertificateTypeService
{
    public interface ICertificateTypeService
    {
        Task<List<CertificateType>> GetAllCertificateTypes();
    }
}
