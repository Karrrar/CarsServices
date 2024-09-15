
namespace CarsServices.Models;

public class Car
{
  public int CarId { get; set; }            // Primary Key
  public string Driver { get; set; }
  public string Owner { get; set; }
  public string Make { get; set; }          // e.g., Toyota
  public string Model { get; set; }         // e.g., Camry
  public int Year { get; set; }

  public string VIN { get; set; }   // Vehicle Identification Number

  // Navigation property: One Car can have many Services
  public List<Service> Services { get; set; }
}