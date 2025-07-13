using DocSpider.Domain.Models;

namespace DocSpider.Domain.DTOs;

public class DocumentDTO
{
    public string? DocumentName { get; set; }
    public string? DocumentDescription { get; set; }
    public string? FileType { get; set; }
    public long FileSize { get; set; }
    public string? FilePath { get; set; }
    public DateTime UploadDate { get; set; }
}
