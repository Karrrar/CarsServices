namespace CarsServices;

public class Car
{
  public int Id { get; set; }
  public string VIN {get; set;}
  public string PlateNumber {get; set;}
  public Person Owner {get; set;}
  public Person Driver {get; set;}
}
