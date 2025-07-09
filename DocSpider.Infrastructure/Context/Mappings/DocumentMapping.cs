using DocSpider.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocSpider.Infrastructure.Context.Mappings;
public class DocumentMapping : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(d => d.Id);

        builder.HasOne(d => d.User)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.User);

        builder.Property(d => d.Id)
            .HasColumnName("DocumentID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .HasDefaultValueSql("NEWID()");

        builder.Property(d => d.Name)
            .IsRequired()
            .HasColumnName("DocumentName")
            .HasColumnType("NVARCHAR(100)")
            .HasMaxLength(100);

        builder.HasIndex(d => d.Name)
            .IsUnique();

        builder.Property(d => d.Description)
            .HasColumnName("DocumentDescription")
            .HasColumnType("NVARCHAR(2000)")
            .HasMaxLength(2000);

        builder.Property(d => d.Type)
            .IsRequired()
            .HasColumnName("FileType")
            .HasColumnType("NVARCHAR(100)")
            .HasMaxLength(100);

        builder.HasIndex(d => d.Type)
            .HasDatabaseName("IX_Documents_FileType");

        builder.Property(d => d.Size)
            .IsRequired()
            .HasColumnName("FileSize")
            .HasColumnType("BIGINT");

        builder.Property(d => d.Path)
            .IsRequired()
            .HasColumnName("FilePath")
            .HasColumnType("NVARCHAR(500)")
            .HasMaxLength(500);

        builder.Property(d => d.Content)
            .IsRequired()
            .HasColumnName("FileContent")
            .HasColumnType("VARBINARY(MAX)");

        builder.Property(d => d.UploadDate)
            .HasColumnName("UploadDate")
            .HasColumnType("DATETIME2")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(d => d.UploadedBy)
            .HasColumnName("UploadedBy")
            .HasColumnType("NVARCHAR(100)")
            .HasMaxLength(100);

        builder.HasIndex(d => d.UploadedBy)
            .HasDatabaseName("IX_Documents_UploadedBy");
    }
}


