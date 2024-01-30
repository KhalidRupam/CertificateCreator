using CertificateCreator.Models;

namespace CertificateCreator.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private HttpClient _httpClient;

        private static string ApiUsers => $"Employee";

        public EmployeeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HttpClient");
        }

        public async Task<List<EmployeeDTO>> GetAllEmployeeDetails()
        {
            try
            {
                var responseMessage = await _httpClient.GetFromJsonAsync<List<EmployeeDTO>>(ApiUsers);

                return responseMessage;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
