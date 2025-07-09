using DocSpider.BuildingBlocks.API;
using DocSpider.Domain.Models;
using DocSpider.Web.Common.Api;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocSpider.Web.Common.Endpoint.Documents
{
    public record UploadDocumentRequest(IFormFile File);

    public record UploadDocumentResponse(Response<Document> DocUploadResponse);
    
    public class UploadDocumentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("", handler: HandleAsync).DisableAntiforgery();


        private static async Task<IResult> HandleAsync(
            [FromForm]UploadDocumentRequest request,
            ISender sender 
        )
        {
            if (request.File == null || request.File.Length == 0)
                return TypedResults.BadRequest(new Response<UploadDocumentRequest>(request, 500, "File is empty"));

            using var memoryStream = new MemoryStream();
            await request.File.CopyToAsync(memoryStream);

            var document = new Document
            {
                Name = request.File.FileName,
                Type = Path.GetExtension(request.File.FileName),
                Size = request.File.Length,
                Content = memoryStream.ToArray(),
                UploadDate = DateTime.Now,
                UploadedBy = null
            };

            var command = document.Adapt<UploadDocumentCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UploadDocumentResponse>();

            return TypedResults.Created($"/{response.DocUploadResponse.Message}");
        }
    }
}
