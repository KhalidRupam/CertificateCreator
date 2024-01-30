namespace CertificateCreator.Services.FileService
{
    public class FileService:IFileService
    {
        private HttpClient _httpClient;

        private static string ApiFile => $"File";

        public FileService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HttpClient");
        }

        public async Task<string> SaveFileToApi(IFormFile formFile)
        {
            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(formFile.OpenReadStream()), "file", formFile.FileName);

                    var responseMessage = await _httpClient.PostAsync(ApiFile, content);

                    return await responseMessage.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
