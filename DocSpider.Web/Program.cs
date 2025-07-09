using DocSpider.Web.Common.Api;
using DocSpider.Web.Common.Endpoint;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataContexts();
builder.AddMediatRDependency();
builder.AddDocumentation();
//builder.Services.AddAntiforgery();

var app = builder.Build();

//app.UseAntiforgery();
app.ConfigureDevEnvironment();
app.MapEndpoints();

app.Run();
