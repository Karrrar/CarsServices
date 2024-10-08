using System.Text.Json.Serialization;

namespace CarsServices.Models;

public class Part
{
  public int PartId { get; set; }           // Primary Key
  public string Name { get; set; }          // e.g., "Oil Filter"
  public string PartNumber { get; set; }

  public decimal Price { get; set; }
  // Navigation property
  // Many-to-Many relationship with Service
  [JsonIgnore]
  public List<Service> ServicesUsedIn { get; set; }
}