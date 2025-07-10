using DocSpider.BuildingBlocks.API;
using DocSpider.BuildingBlocks.CQRS;
using DocSpider.Domain.Models;
using DocSpider.Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DocSpider.Web.Common.Endpoint.Documents
{
    public record UploadDocumentCommand(Document Document) : ICommand<UploadDocumentResult>;

    public record UploadDocumentResult(Response<bool> UploadSuccess);

    public class UploadDocumentHandler(AppDbContext Context) : ICommandHandler<UploadDocumentCommand, UploadDocumentResult>
    {
        public async Task<UploadDocumentResult> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var doc = request.Document.Adapt<Document>();

            var user = await Context
                .Users
                .FirstOrDefaultAsync(u => u.UserId == doc.UserId, cancellationToken: cancellationToken);

            doc.User = user;

            await Context.Documents.AddAsync(doc, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            return new UploadDocumentResult(new Response<bool>(true, 201, "Document Uploaded"));
        }
    }
}
