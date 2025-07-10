using DocSpider.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocSpider.Infrastructure.Context.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserId)
            .HasDefaultValueSql("NEWID()");

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(u => u.UserName)
            .HasDatabaseName("IX_Users_UserName");
    }
}

