namespace DocSpider.Domain.Models.Request;

public record GetDocumentRequest(int PageNumber = 0, int PageSize = 25);
