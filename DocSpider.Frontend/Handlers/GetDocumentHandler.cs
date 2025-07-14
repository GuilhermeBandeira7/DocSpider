using DocSpider.Domain.DTOs;
using DocSpider.Domain.Models;
using DocSpider.Domain.Interfaces;
using System.Net.Http.Json;
using DocSpider.Domain.Models.Request;

namespace DocSpider.Frontend.Handlers
{
    public class GetDocumentHandler(IHttpClientFactory httpClientFactory) : IGetDocumentHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);       

        public async Task<Response<List<DocumentDTO>>> GetDocumentsAsync(GetDocumentRequest request)
        {
            try
            {
                var result = await _client.GetFromJsonAsync<Response<List<DocumentDTO>>>("v1/documents/get-documents")
                    ?? new Response<List<DocumentDTO>>(null, 400, "Não foi possível obter documents"); 

                return result;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new Response<List<DocumentDTO>>(null, 500, ex.Message);
            }
        }
         
    }
}
