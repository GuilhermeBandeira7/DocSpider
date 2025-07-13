using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DocSpider.Frontend;
using MudBlazor.Services;
using DocSpider.Domain.Interfaces;
using DocSpider.Frontend.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configuration.HttpClientName, opt => { opt.BaseAddress = new Uri(Configuration.BackendUrl); });

builder.Services.AddTransient<IGetDocumentHandler, GetDocumentHandler>();
builder.Services.AddTransient<IUploadHandler, UploadHandler>();

await builder.Build().RunAsync();
