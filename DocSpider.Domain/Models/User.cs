namespace DocSpider.Domain.Models;
public class User
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public ICollection<Document> Documents { get; set; } = [];
}

