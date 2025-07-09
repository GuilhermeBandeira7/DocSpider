using DocSpider.Infrastructure.Context;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;

namespace DocSpider.Web.Common.Api;

public static class BuilderExtensions
{
    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(""));
        });
    }

    public static void AddMediatRDependency(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            //config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            //config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
       builder.Services.AddSwaggerGen(opts => 
       { 
           opts.CustomSchemaIds(n => n.FullName);
           //opts.OperationFilter<AntiforgeryApplicationBuilderExtensions>();
       });
    }
}

