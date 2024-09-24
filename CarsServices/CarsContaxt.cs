using CarsServices.Models;
using Microsoft.EntityFrameworkCore;

public class CarsContaxt : DbContext
{
  public CarsContaxt(DbContextOptions<CarsContaxt> options) : base(options)
  {

  }
  public DbSet<Car> Cars { get; set; }
  public DbSet<Part> Parts { get; set; }
  public DbSet<Service> Services { get; set; }

}