using DocSpider.Domain.DTOs;
using DocSpider.Domain.Interfaces;
using DocSpider.Domain.Models;
using DocSpider.Domain.Models.Request;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DocSpider.Frontend.Handlers
{
    public class UploadHandler(IHttpClientFactory httpClientFactory) : IUploadHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<string>> UploadFiles(UploadDocumentRequest request)
        {
            try
            {
                using var formContent = new MultipartFormDataContent();

                // Add the file to the request
                using var fileStream = request.File.OpenReadStream();
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(request.File.ContentType);

                // "file" should match the parameter name in your endpoint (UploadDocumentRequest.File)
                formContent.Add(fileContent, name: "file", fileName: request.File.FileName);

                // Send the request
                var response = await _client.PostAsync("v1/documents/upload", formContent);

                // Ensure success before parsing
                response.EnsureSuccessStatusCode();

                // Parse the JSON response
                return await response.Content.ReadFromJsonAsync<Response<string>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<string>(null, 500, ex.Message);
            }
        }
    }
}
