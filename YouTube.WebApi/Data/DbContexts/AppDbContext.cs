using Microsoft.EntityFrameworkCore;
using YouTube.WebApi.Domain.Entities;

namespace YouTube.WebApi.Data.DbContexts;

public class AppDbContext : DbContext
{
    public virtual DbSet<Video> Videos { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(email => email.Email)
            .IsUnique();
    }
}
