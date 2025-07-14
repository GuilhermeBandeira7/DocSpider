using DocSpider.BuildingBlocks.CQRS;
using DocSpider.Domain.Models;
using DocSpider.Infrastructure.Context;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DocSpider.Web.Common.Endpoint.Documents.Upload
{
    public record UploadDocumentCommand(Document Document) : ICommand<Response<string>>;

    public class UploadDocumentCommandValidator : AbstractValidator<UploadDocumentCommand>
    {
        public UploadDocumentCommandValidator()
        {
            RuleFor(f => f.Document.FileContent)
            .NotNull()
            .Must(f => f?.Length > 0)
            .WithMessage("File cannot be empty");

            RuleFor(f => f.Document.DocumentName)
            .NotNull()
            .WithMessage("File name cannot be empty")
            .Must(BeAllowedFileType)
            .WithMessage("File name cannot be empty");
        }

        private bool BeAllowedFileType(string? fileName)
        {
            var restrictedExtensions = new[] { ".exe", ".zip", ".bat" };
            var fileExtension = Path.GetExtension(fileName)?.ToLowerInvariant();
            return !restrictedExtensions.Contains(fileExtension);
        }
    }

    public class UploadDocumentHandler(AppDbContext Context)
        : ICommandHandler<UploadDocumentCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(UploadDocumentCommand command, CancellationToken cancellationToken)
        {
            var doc = command.Document.Adapt<Document>();

            var user = await Context
                .Users
                .FirstOrDefaultAsync(u => u.UserId == doc.UserId, cancellationToken: cancellationToken);

            doc.User = user;

            await Context.Documents.AddAsync(doc, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            return new Response<string>(doc.DocumentName ,201, "Document Uploaded");
        }
    }
}
