using CertificateCreator.Models;

namespace CertificateCreator.Services.CertificateTypeService
{
    public class CertificateTypeService:ICertificateTypeService
    {
        private HttpClient _httpClient;

        private static string ApiUsers => $"CertificateType";

        public CertificateTypeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HttpClient");
        }

        public async Task<List<CertificateType>> GetAllCertificateTypes()
        {
            try
            {
                var responseMessage = await _httpClient.GetFromJsonAsync<List<CertificateType>>(ApiUsers);

                return responseMessage;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
