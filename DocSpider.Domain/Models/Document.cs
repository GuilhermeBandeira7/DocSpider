namespace DocSpider.Domain.Models;
public class Document
{
    public Guid DocumentId { get; set; }
    public string? DocumentName { get; set; }
    public string? DocumentDescription { get; set; }
    public string? FileType { get; set; }
    public long FileSize { get; set; }
    public string? FilePath { get; set; }
    public byte[]? FileContent { get; set; }
    public DateTime UploadDate { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; } = new();
}

