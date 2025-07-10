using DocSpider.Domain.Models;

namespace DocSpider.Domain.Entities;

public interface IDomainDocumentFactory
{
    Document Create(string name, string type, long size, byte[] content, DateTime uploadDate, Guid userId);
}
