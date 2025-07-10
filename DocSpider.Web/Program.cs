using DocSpider.Web.Common.Api;
using DocSpider.Web.Common.Endpoint;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataContexts();
builder.AddMediatRDependency();
builder.AddDocumentation();

var app = builder.Build();

app.ConfigureDevEnvironment();
app.MapEndpoints();

app.Run();
