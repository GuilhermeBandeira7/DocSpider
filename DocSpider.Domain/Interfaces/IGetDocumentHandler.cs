using DocSpider.Domain.DTOs;
using DocSpider.Domain.Models;
using DocSpider.Domain.Models.Request;

namespace DocSpider.Domain.Interfaces;

public interface IGetDocumentHandler
{
    Task<Response<List<DocumentDTO>>> GetDocumentsAsync(GetDocumentRequest request);
}
