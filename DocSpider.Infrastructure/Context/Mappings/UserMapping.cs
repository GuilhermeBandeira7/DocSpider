using DocSpider.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocSpider.Infrastructure.Context.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.Documents)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.Id);

        builder.Property(u => u.Id)
            .HasColumnName("UserID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .HasDefaultValueSql("NEWID()");

        builder.Property(u => u.Name)
            .HasColumnName("UserName")
            .HasColumnType("NVARCHAR(50)");
    }
}

