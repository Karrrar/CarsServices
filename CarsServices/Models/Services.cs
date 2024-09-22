using CarsServices.Models;

namespace CarsServices.Models;

public class Service
{
  public int ServiceId { get; set; }        // Primary Key
  public int CarId { get; set; }            // Foreign Key to Car

  public DateTime Date { get; set; }
  public string Description { get; set; }   // e.g., "Oil Change"

  // Navigation properties

  // The Car that was serviced
  public Car Car { get; set; }

  // Many-to-Many relationship with Part
  public List<Part> PartsUsed { get; set; }
}