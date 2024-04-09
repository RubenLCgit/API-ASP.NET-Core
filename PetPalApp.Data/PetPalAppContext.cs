using Microsoft.EntityFrameworkCore;
using PetPalApp.Domain;
using Microsoft.Extensions.Logging;

namespace PetPalApp.Data;

public class PetPalAppContext : DbContext
{
  public PetPalAppContext(DbContextOptions<PetPalAppContext> options) : base(options)
  {
  }

  public DbSet<User> Users { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Service> Services { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder
      .LogTo(Console.WriteLine, LogLevel.Information)
      .EnableSensitiveDataLogging();
  }

}