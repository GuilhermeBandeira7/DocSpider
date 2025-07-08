namespace DocSpider.Domain.Models;
public class Document
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public long? Size { get; set; }
    public string? Path { get; set; }
    public byte[]? Content { get; set; }
    public DateTime? UploadDate { get; set; }
    public string? UploadedBy { get; set; }
}

