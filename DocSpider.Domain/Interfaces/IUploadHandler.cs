

using DocSpider.Domain.Models;
using DocSpider.Domain.Models.Request;

namespace DocSpider.Domain.Interfaces;

public interface IUploadHandler
{
    Task<Response<string>> UploadFiles(UploadDocumentRequest request);
}
