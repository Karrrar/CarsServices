
using CarsServices;
using Microsoft.EntityFrameworkCore;

public class CarsDbContext : DbContext
{
  public DbSet<Person> Drivers { get; set; }
  public DbSet<Car> Cars { get; set; }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=cars.db");
  }
}