using DocSpider.BuildingBlocks.API;
using DocSpider.BuildingBlocks.CQRS;
using DocSpider.Domain.Models;
using DocSpider.Infrastructure.Context;
using Mapster;

namespace DocSpider.Web.Common.Endpoint.Documents
{
    public record UploadDocumentCommand(Document Document) : ICommand<UploadDocumentResult>;

    public record UploadDocumentResult(Response<Document> DocUploadResponse);

    public class UploadDocumentHandler(AppDbContext Context) : ICommandHandler<UploadDocumentCommand, UploadDocumentResult>
    {
        public async Task<UploadDocumentResult> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var doc = request.Document.Adapt<Document>(); 
            
            await Context.Documents.AddAsync(doc, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            return new UploadDocumentResult(new Response<Document>(doc, 201, "Document Uploaded"));
        }
    }
}
