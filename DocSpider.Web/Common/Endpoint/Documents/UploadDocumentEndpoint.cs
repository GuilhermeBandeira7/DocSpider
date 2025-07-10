using DocSpider.BuildingBlocks.API;
using DocSpider.Domain.Models;
using DocSpider.Web.Common.Api;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocSpider.Web.Common.Endpoint.Documents
{
    public record UploadDocumentRequest(IFormFile File);

    public record UploadDocumentResponse(Response<bool> UploadSuccess);
    
    public class UploadDocumentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("", handler: HandleAsync).DisableAntiforgery();

        private static async Task<IResult> HandleAsync(
            [FromForm]UploadDocumentRequest request,
            ISender sender 
        )
        {
            //if (request.File == null || request.File.Length == 0)
            //    return TypedResults.BadRequest(new Response<UploadDocumentRequest>(request, 500, "File is empty"));

            //var restrictedExtensions = new[] { ".exe", ".zip", ".bat", ".cmd" };
            //var fileExtension = Path.GetExtension(request.File.FileName).ToLowerInvariant();

            //if (restrictedExtensions.Contains(fileExtension))
            //    return TypedResults.BadRequest($"File type {fileExtension} is not allowed.");
            
            using var memoryStream = new MemoryStream();
            await request.File.CopyToAsync(memoryStream);

            var document = new Document
            {
                DocumentName = request.File.FileName,
                FileType = Path.GetExtension(request.File.FileName),
                FileSize = request.File.Length,
                FileContent = memoryStream.ToArray(),
                UploadDate = DateTime.Now,
                UserId = new Guid("C325B3E6-C92D-4F92-BCE2-D690A9248E13") 
            };

            var command =  new UploadDocumentCommand(document);

            var result = await sender.Send(command);

            var response = result.Adapt<UploadDocumentResponse>();

            return TypedResults.Created($"/{response.UploadSuccess.Message}");
        }


    }
}
