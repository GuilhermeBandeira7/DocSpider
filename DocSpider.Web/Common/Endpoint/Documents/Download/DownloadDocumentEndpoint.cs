using DocSpider.Domain.Models;
using DocSpider.Web.Common.Api;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocSpider.Web.Common.Endpoint.Documents.Download
{
    public record DownloadDocumentRequest(Guid DocumentId);
    public class DownloadDocumentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/download", handler: HandleAsync)
            .DisableAntiforgery()
            .Produces(StatusCodes.Status200OK, typeof(File))
            .Produces(StatusCodes.Status404NotFound);

        private static async Task<IResult> HandleAsync(
            [FromForm] DownloadDocumentRequest request,
            ISender sender
        )
        {
            var command = request.Adapt<DownloadDocumentCommand>();

            var response = await sender.Send(command);

            return response.IsSuccess 
                ? TypedResults.File(
                        response.Data.FileContent,
                        response.Data?.FileType,
                        response.Data?.DocumentName)
                : TypedResults.BadRequest();
        }
    }
}
