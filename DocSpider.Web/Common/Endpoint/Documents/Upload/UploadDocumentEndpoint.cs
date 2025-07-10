using DocSpider.Application.Common.Interfaces;
using DocSpider.Web.Common.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocSpider.Web.Common.Endpoint.Documents.Upload
{
    public record UploadDocumentRequest(IFormFile File);

    public class UploadDocumentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/upload", handler: HandleAsync).DisableAntiforgery();

        private static async Task<IResult> HandleAsync(
            [FromForm] UploadDocumentRequest request,
            IDocumentFactory documentFactory,
            ISender sender
        )
        {
            using var memoryStream = new MemoryStream();
            await request.File.CopyToAsync(memoryStream);

            var document = documentFactory.CreateFromFile(request.File, new Guid("C325B3E6-C92D-4F92-BCE2-D690A9248E13"));

            var command = new UploadDocumentCommand(document.Result);

            var result = await sender.Send(command);

            return TypedResults.Created($"{result.Message}");
        }
    }
}
