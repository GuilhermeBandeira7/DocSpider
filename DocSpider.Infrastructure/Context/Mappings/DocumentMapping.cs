using DocSpider.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocSpider.Infrastructure.Context.Mappings;
public class DocumentMapping : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(d => d.DocumentId);

        builder.Property(d => d.DocumentId)
            .HasDefaultValueSql("NEWID()");

        builder.Property(d => d.DocumentName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.DocumentDescription)
            .HasMaxLength(2000);

        builder.Property(d => d.FileType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.FileSize)
            .IsRequired();

        builder.Property(d => d.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(d => d.FileContent)
            .IsRequired();

        builder.Property(d => d.UploadDate)
            .HasDefaultValueSql("GETDATE()");

        builder.HasIndex(d => d.DocumentName)
            .IsUnique()
            .HasDatabaseName("UQ_Documents_DocumentName");

        builder.HasOne(d => d.User)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasIndex(d => d.UserId)
            .HasDatabaseName("IX_Documents_UserID");

        builder.HasIndex(d => d.FileType)
            .HasDatabaseName("IX_Documents_FileType");
    }
}


