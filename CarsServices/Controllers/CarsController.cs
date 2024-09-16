using CarsServices.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        static private CarsContaxt db;
        
        public CarsController()
        {
            db = new CarsContaxt();
        }
        // GET: api/<CarsController>
        [HttpGet]
        public List<Car> GetCars()
        {
            return db.Cars.ToList();
        }
        
       [HttpGet("Parts")]
        public List<Part> GetGetParts()
        {
            return db.Parts.ToList();
        }


        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public Car Get(int id)
        {
            return db.Cars.Where(x => x.CarId == id).OrderBy(x => x.Year).First();
        }

        // POST api/<CarsController>
        [HttpPost]
        public void Post([FromBody] Car newCar)
        {
            db.Add(newCar);
            db.SaveChanges();
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
            var carRoDelete = db.Cars.Where(x => x.CarId == id).First();

            db.Cars.Remove(carRoDelete);
            db.SaveChanges();
        }
    }
}
