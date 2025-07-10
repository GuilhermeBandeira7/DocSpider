using DocSpider.BuildingBlocks.API;
using DocSpider.BuildingBlocks.CQRS;
using DocSpider.Domain.Models;
using DocSpider.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DocSpider.Web.Common.Endpoint.Documents.Download
{
    public record DownloadDocumentCommand(Guid DocumentId) : ICommand<Response<Document>>;

    public class DownloadDocumentHandler(AppDbContext Context)
        : ICommandHandler<DownloadDocumentCommand, Response<Document>>
    {
        public async Task<Response<Document>> Handle(DownloadDocumentCommand command, CancellationToken cancellationToken)
        {
            var document = await Context.Documents
                .FirstOrDefaultAsync(d => d.DocumentId == command.DocumentId, cancellationToken: cancellationToken);

            if (document == null || !Path.HasExtension(document.DocumentName))
                return new Response<Document>(null, 404, "Document Not Found");

            return new Response<Document>(document, 200, "Document Downloaded Successfully");
        }
    }
}
