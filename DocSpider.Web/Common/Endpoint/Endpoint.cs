using DocSpider.Web.Common.Api;
using DocSpider.Web.Common.Endpoint.Documents.Download;
using DocSpider.Web.Common.Endpoint.Documents.Upload;

namespace DocSpider.Web.Common.Endpoint
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app
           .MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("v1/documents")
            .WithTags("Documents")
            .MapEndpoint<UploadDocumentEndpoint>()
            .MapEndpoint<DownloadDocumentEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
