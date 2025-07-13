using DocSpider.Web;
using DocSpider.Web.Common.Api;
using DocSpider.Web.Common.Endpoint;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataContexts();
builder.AddMediatRDependency();
builder.AddDocumentation();
builder.AddValidations();
builder.AddServices();
builder.AddCrossOrigin();

var app = builder.Build();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.ConfigureDevEnvironment();
app.MapEndpoints();

app.Run();
