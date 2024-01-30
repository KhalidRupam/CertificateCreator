namespace CertificateCreator.Services.FileService
{
    public interface IFileService
    {
        Task<string> SaveFileToApi(IFormFile formFile);
    }
}
