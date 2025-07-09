using Microsoft.EntityFrameworkCore;
using DocSpider.Domain.Models;
using System.Reflection;

namespace DocSpider.Infrastructure.Context;
public class AppDbContext(DbContextOptions<AppDbContext> Options) : DbContext(Options)
{
    public DbSet<Document> Documents { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

