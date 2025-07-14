using DocSpider.Application.Common.Interfaces;                                                      
using DocSpider.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace DocSpider.Application.Features.Documents.Factories;

public class DocumentFactory : IDocumentFactory
{
    public async Task<Document> CreateFromFile(IFormFile file, Guid userId)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        return new Document
        {
            DocumentName = file.FileName,
            DocumentDescription = "Test document creation",
            FileType = Path.GetExtension(file.FileName),
            FileSize = file.Length,
            FilePath = Path.GetFullPath(file.FileName),
            FileContent = ms.ToArray(),
            UploadDate = DateTime.Now,
            UserId = userId
        };
    }
}
