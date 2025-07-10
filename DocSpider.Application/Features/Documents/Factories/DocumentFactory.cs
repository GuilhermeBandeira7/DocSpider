using DocSpider.Application.Common.Interfaces;
using DocSpider.Domain.Entities;
using DocSpider.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace DocSpider.Application.Features.Documents.Factories;

public class DocumentFactory : IDocumentFactory
{
    private readonly IDomainDocumentFactory _domainFactory;
    public async Task<Document> CreateFromFile(IFormFile file, Guid userId)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        return _domainFactory.Create(
            file.FileName,
            Path.GetExtension(file.FileName),
            file.Length,
            ms.ToArray(),
            DateTime.Now,
            userId);
    }
}
