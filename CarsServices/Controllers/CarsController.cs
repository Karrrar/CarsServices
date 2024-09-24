using CarsServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private CarsContaxt db;

        public CarsController(CarsContaxt carsContaxt)
        {
            db = carsContaxt;
        }

        // GET: api/<CarsController>
        [HttpGet]
        public async Task<List<Car>> GetCars()
        {
            return await db.Cars.ToListAsync();
        }


        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public async Task<Car> Get(int id)
        {
            return await db.Cars.Where(x => x.CarId == id)
                        .OrderBy(x => x.Year)
                        .FirstOrDefaultAsync();
        }

        // POST api/<CarsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Car newCar)
        {
            // Validate the newCar object
            var validationErrors = ValidateCar(newCar);

            if (validationErrors.Length > 0)
            {
                // Return 400 Bad Request with the validation errors
                return BadRequest(new { Errors = validationErrors });
            }

            // If validation passes, add the car to the database
            db.Add(newCar);
            await db.SaveChangesAsync();

            // Return 201 Created with the newly created car
            return CreatedAtAction(nameof(Get), new { id = newCar.CarId }, newCar);
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Car updatedCar)
        {
            // Validate the updatedCar object
            var validationErrors = ValidateCar(updatedCar);
            if (validationErrors.Length > 0)
            {
                // Return 400 Bad Request with the validation errors
                return BadRequest(new { Errors = validationErrors });
            }

            // Find the existing car by its ID
            var existingCar = await db.Cars.FindAsync(id);
            if (existingCar == null)
            {
                return NotFound();
            }

            // Update the car properties
            existingCar.Driver = updatedCar.Driver;
            existingCar.Owner = updatedCar.Owner;
            existingCar.Make = updatedCar.Make;
            existingCar.Model = updatedCar.Model;
            existingCar.Year = updatedCar.Year;

            // Save changes to the database
            db.Cars.Update(existingCar);
            await db.SaveChangesAsync();

            // Return 204 No Content to indicate the update was successful
            return NoContent();
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Find and return the car from the Database
            var carToDelete = await db.Cars.FindAsync(id);
            if (carToDelete is null)
            {
                return NotFound();
            }

            // Remove the car for the database
            db.Cars.Remove(carToDelete);
            // Save the changes to the database
            await db.SaveChangesAsync();
            // Return Status code 200
            return Ok();
        }

        private static string[] ValidateCar(Car car)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(car.Make))
            {
                errors.Add("Make is required.");
            }
            else if (car.Make.Length <= 2)
            {
                errors.Add("Make must be longer than 2 characters.");
            }

            if (string.IsNullOrEmpty(car.Model))
            {
                errors.Add("Model is required.");
            }
            else if (car.Model.Length <= 2)
            {
                errors.Add("Model must be longer than 2 characters.");
            }

            if (car.Year <= 0)
            {
                errors.Add("Year must be a positive number.");
            }

            // For example, validate that the year is within a reasonable range
            if (car.Year < 1886 || car.Year > DateTime.Now.Year)
            {
                errors.Add("Year must be between 1886 and the current year.");
            }

            return errors.ToArray();
        }
    }
}
