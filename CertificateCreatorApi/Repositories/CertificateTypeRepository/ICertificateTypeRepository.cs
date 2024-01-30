using CertificateCreatorApi.Models;

namespace CertificateCreatorApi.Repositories.CertificateTypeRepository
{
    public interface ICertificateTypeRepository
    {
        Task<List<CertificateType>> GetAllCertificateTypes();
    }
}
