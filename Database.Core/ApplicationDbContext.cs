using Database.Core.Entities;
using Database.Core.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Database.Core;

public class ApplicationDbContext(
	DbContextOptions<ApplicationDbContext> options)
	: DbContext(options)
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("devtips_multiple_migrations");

        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
