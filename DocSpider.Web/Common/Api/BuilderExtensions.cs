using DocSpider.Application.Common.Interfaces;
using DocSpider.Application.Features.Documents.Factories;
using DocSpider.BuildingBlocks.Behaviours;
using DocSpider.Infrastructure.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DocSpider.Web.Common.Api;

public static class BuilderExtensions
{
    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
    }

    public static void AddMediatRDependency(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
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

    public static void AddValidations(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDocumentFactory, DocumentFactory>();
    }
}

