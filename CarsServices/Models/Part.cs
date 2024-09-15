namespace CarsServices.Models;

public class Part
{
  public int PartId { get; set; }           // Primary Key
  public string Name { get; set; }          // e.g., "Oil Filter"
  public string PartNumber { get; set; }

  // Navigation property
  // Many-to-Many relationship with Service
  public List<Service> ServicesUsedIn { get; set; }
}