using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        static private List<Car> cars;
        public CarsController()
        {
            cars = new List<Car>();

            Car carOne = new Car();
            carOne.Id = 1;
            carOne.VIN = "H23963y32y4234uy434";
            carOne.PlateNumber = "11 M 12328";

            carOne.Owner = new Person();
            carOne.Owner.Name = "Ali";
            Person driveOne = new Person();
            carOne.Driver = driveOne;
            carOne.Driver.Departmetn = "3445";

            carOne.Driver.Name = "Mohammad";


            Car carTwo = new Car();
            carTwo.Id = 1;
            carTwo.VIN = "H23978872342";
            carTwo.PlateNumber = "11 M 478833";

            carTwo.Owner = new Person();
            carTwo.Owner.Name = "Huda";
            //Person driveTwo = new Person();
            //carTwo.Driver = driveTwo;
            //carTwo.Driver.Departmetn = "3445";

            //carTwo.Driver.Name = "Nour";

            cars.Add(carOne);
            cars.Add(carTwo);
        }
        // GET: api/<CarsController>
        [HttpGet]
        public List<Car> GetCars()
        {
            return cars;
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CarsController>
        [HttpPost]
        public void Post([FromBody] Car newCar)
        {
            cars.Add(newCar);
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
