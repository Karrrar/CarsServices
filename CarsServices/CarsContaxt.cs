using CarsServices.Models;
using Microsoft.EntityFrameworkCore;

class CarsContaxt : DbContext
{
  public DbSet<Car> Cars { get; set; }
  public DbSet<Part> Parts { get; set; }
  public DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=cars;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=True");
    }
}