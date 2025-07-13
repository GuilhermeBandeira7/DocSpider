using Microsoft.AspNetCore.Http;

namespace DocSpider.Domain.Models.Request;

public record UploadDocumentRequest(IFormFile File);

