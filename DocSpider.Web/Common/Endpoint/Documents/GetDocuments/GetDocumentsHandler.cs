using DocSpider.BuildingBlocks.CQRS;
using DocSpider.Domain.DTOs;
using DocSpider.Domain.Interfaces;
using DocSpider.Domain.Models;
using DocSpider.Domain.Models.Request;
using DocSpider.Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;

namespace DocSpider.Web.Common.Endpoint.Documents.GetDocuments
{
    public record GetDocumentsCommand(int PageNumber, int PageSize) : ICommand<Response<List<DocumentDTO>>>;
    public class GetDocumentsHandler(AppDbContext Context)
        : ICommandHandler<GetDocumentsCommand, Response<List<DocumentDTO>>> 
    {
        public async Task<Response<List<DocumentDTO>>> Handle(GetDocumentsCommand command, CancellationToken cancellationToken)
        {
            var userId = new Guid("C325B3E6-C92D-4F92-BCE2-D690A9248E13");

            var query = Context
                .Documents
                .AsNoTracking();
            var documents = await query
            .Where(x => x.UserId == userId)
                .Skip((command.PageNumber - 0) * command.PageSize)
                .Take(command.PageSize)
                .ToListAsync(cancellationToken: cancellationToken);

            var count = await query.CountAsync();

            var documentsDTO = documents.Adapt<List<DocumentDTO>>();

            return new Response<List<DocumentDTO>>(documentsDTO, 200, $"{count} records returned");
        }
    }
}
