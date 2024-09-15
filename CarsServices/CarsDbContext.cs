
using CarsServices;
using Microsoft.EntityFrameworkCore;

public class CarsDbContext : DbContext
{
  public DbSet<Person> Drivers { get; set; }
  public DbSet<Car> Cars { get; set; }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer("Server=localhost;Database=cars;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=True;");
  }
}