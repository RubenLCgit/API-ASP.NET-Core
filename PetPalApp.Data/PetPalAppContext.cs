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
    modelBuilder.Entity<User>().HasData(
      new User { UserId = 1, UserRole = "Admin", UserName = "Ruben", UserEmail = "ruben@gmail.com.com", UserPassword = "patatas1", UserSupplier = true },
      new User { UserId = 2, UserRole = "Client", UserName = "Xio", UserEmail = "xio@gmail.com", UserPassword = "patatas2", UserSupplier = false },
      new User { UserId = 3, UserRole = "Client", UserName = "Carlota", UserEmail = "carlota@gmail.com", UserPassword = "patatas3", UserSupplier = false },
      new User { UserId = 4, UserRole = "Client", UserName = "Alberto", UserEmail = "alberto@gmail.com", UserPassword = "patatas4", UserSupplier = true },
      new User { UserId = 5, UserRole = "Client", UserName = "Alejandro", UserEmail = "alejandro@gmail.com", UserPassword = "patatas5", UserSupplier = true }
    );

    modelBuilder.Entity<Product>().HasData(
      new Product { ProductId = 1, UserId = 1, ProductType = "Food", ProductName = "Dog food", ProductDescription = "Dog food for dogs", ProductPrice = 10.0M, ProductAvailability = new DateTime(2022, 1, 1), ProductOnline = true, ProductStock = 10, ProductRating = 4.5 },
      new Product { ProductId = 2, UserId = 2, ProductType = "Toy", ProductName = "Cat toy", ProductDescription = "Interactive cat toy", ProductPrice = 15.0M, ProductAvailability = new DateTime(2022, 2, 1), ProductOnline = true, ProductStock = 20, ProductRating = 4.8 },
      new Product { ProductId = 3, UserId = 3, ProductType = "Accessory", ProductName = "Leash", ProductDescription = "Durable dog leash", ProductPrice = 20.0M, ProductAvailability = new DateTime(2022, 3, 1), ProductOnline = true, ProductStock = 30, ProductRating = 4.7 },
      new Product { ProductId = 4, UserId = 4, ProductType = "Food", ProductName = "Parrot food", ProductDescription = "Nutritional food for parrots", ProductPrice = 25.0M, ProductAvailability = new DateTime(2022, 4, 1), ProductOnline = true, ProductStock = 40, ProductRating = 4.9 },
      new Product { ProductId = 5, UserId = 5, ProductType = "Grooming", ProductName = "Shampoo", ProductDescription = "Organic pet shampoo", ProductPrice = 30.0M, ProductAvailability = new DateTime(2022, 5, 1), ProductOnline = true, ProductStock = 50, ProductRating = 4.6 }
    );

    modelBuilder.Entity<Service>().HasData(
      new Service { ServiceId = 1, UserId = 1, ServiceType = "Grooming", ServiceName = "Basic Grooming", ServiceDescription = "Basic pet grooming service", ServicePrice = 50.0M, ServiceAvailability = new DateTime(2022, 6, 1), ServiceOnline = true, ServiceRating = 4.5 },
      new Service { ServiceId = 2, UserId = 2, ServiceType = "Training", ServiceName = "Obedience Training", ServiceDescription = "Basic obedience training for dogs", ServicePrice = 200.0M, ServiceAvailability = new DateTime(2022, 7, 1), ServiceOnline = true, ServiceRating = 4.8 },
      new Service { ServiceId = 3, UserId = 3, ServiceType = "Sitting", ServiceName = "Pet Sitting", ServiceDescription = "Pet sitting for all kinds of pets", ServicePrice = 30.0M, ServiceAvailability = new DateTime(2022, 8, 1), ServiceOnline = true, ServiceRating = 4.7 },
      new Service { ServiceId = 4, UserId = 4, ServiceType = "Walking", ServiceName = "Dog Walking", ServiceDescription = "Daily dog walking service", ServicePrice = 15.0M, ServiceAvailability = new DateTime(2022, 9, 1), ServiceOnline = true, ServiceRating = 4.9 }
    );
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder
      .LogTo(Console.WriteLine, LogLevel.Information)
      .EnableSensitiveDataLogging();
  }
}