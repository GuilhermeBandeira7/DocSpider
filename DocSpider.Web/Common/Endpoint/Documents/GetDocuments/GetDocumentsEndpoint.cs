using DocSpider.Domain.Interfaces;
using DocSpider.Domain.Models.Request;
using DocSpider.Web.Common.Api;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocSpider.Web.Common.Endpoint.Documents.GetDocuments
{
    public class GetDocumentsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/get-documents", handler: HandleAsync)
            .DisableAntiforgery()
            .Produces(StatusCodes.Status200OK, typeof(File))
            .Produces(StatusCodes.Status404NotFound);

        private static async Task<IResult> HandleAsync(
            ISender sender,
            [FromQuery] int pageNumber = 0,
            [FromQuery] int pageSize = 25          
        )
        {
            var command = new GetDocumentsCommand(pageNumber, pageSize);

            var result = await sender.Send(command);

            return result.IsSuccess
               ? TypedResults.Ok(result)
               : TypedResults.BadRequest(result);
        }
    }
}
