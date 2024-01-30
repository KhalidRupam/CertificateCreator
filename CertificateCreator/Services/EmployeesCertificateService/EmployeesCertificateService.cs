using CertificateCreator.Models;

namespace CertificateCreator.Services.EmployeesCertificateService
{
    public class EmployeesCertificateService:IEmployeesCertificateService
    {
        private HttpClient _httpClient;

        private static string ApiUsers => $"EmployeesCertificate";

        public EmployeesCertificateService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HttpClient");
        }

        public async Task<string> insertEmployeesCertificate(EmployeesCertificate employeesCertificate)
        {
            try
            {
                var responseMessage = await _httpClient.PostAsJsonAsync(ApiUsers, employeesCertificate);
                return await responseMessage.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> updateEmployeesCertificate(EmployeesCertificate employeesCertificate)
        {
            try
            {
                var responseMessage = await _httpClient.PutAsJsonAsync(ApiUsers, employeesCertificate);
                return await responseMessage.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EmployeesCertificatesWithDetails>> GetAllEmployeesCertificates()
        {
            try
            {
                
                var responseMessage = await _httpClient.GetAsync(ApiUsers);
                var res = await responseMessage.Content.ReadFromJsonAsync<List<EmployeesCertificatesWithDetails>>();
                return res;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EmployeesCertificatesWithDetails>> GetAllEmployeesCertificatesByEmployeeId(string creatorId)
        {
            try
            {

                var responseMessage = await _httpClient.GetAsync(ApiUsers+ "/GetAllEmployeesCertificatesByEmployeeId?creatorId=" + creatorId);
                var res = await responseMessage.Content.ReadFromJsonAsync<List<EmployeesCertificatesWithDetails>>();
                return res;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> DeleteEmployeesCertificates(int id)
        {
            try
            {
                var url = ApiUsers + "?id=" + id;
                var responseMessage = await _httpClient.DeleteAsync(url);
                var res = await responseMessage.Content.ReadAsStringAsync();
                return res;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
