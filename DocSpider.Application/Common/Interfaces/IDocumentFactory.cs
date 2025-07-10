using DocSpider.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace DocSpider.Application.Common.Interfaces;

public interface IDocumentFactory
{
    Task<Document> CreateFromFile(IFormFile file, Guid userId);
}
