using CarsServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        static private CarsContaxt db;

        public PartsController(CarsContaxt carsContaxt)
        {
            db = carsContaxt;
        }

        // GET: api/<PartsController>
        [HttpGet]
        public async Task<List<Part>> GetParts()
        {
            return await db.Parts.ToListAsync();
        }

        // GET api/<PartsController>/5
        [HttpGet("{id}")]
        public async Task<Part> Get(int id)
        {
            return await db.Parts.Where(c => c.PartId == id).FirstOrDefaultAsync();
        }

        // GET api/<PartsController>/PartsPrices
        [HttpGet("PartsPrices")]
        public async Task<List<string>> GetPartsPrices()
        {
            return await db.Parts
                .Where(s => s.Price > 200 && s.Price < 300)
                .Select(x => x.Name)
                .ToListAsync();
        }

        // GET api/<PartsController>/PartsNames
        [HttpGet("PartsNames")]
        public async Task<List<string>> GetPartsNames()
        {
            return await db.Parts.Select(x => x.Name).ToListAsync();
        }

        // POST api/<PartsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Part newPart)
        {
            // Validate the newPart object
            var validationErrors = ValidatePart(newPart);

            if (validationErrors.Length > 0)
            {
                // Return 400 Bad Request with the validation errors
                return BadRequest(new { Errors = validationErrors });
            }

            await db.Parts.AddAsync(newPart);
            await db.SaveChangesAsync();

            // Return 201 Created with the newly created part
            return CreatedAtAction(nameof(Get), new { id = newPart.PartId }, newPart);
        }

        // PUT api/<PartsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Part updatedPart)
        {
            // Validate the updatedPart object
            var validationErrors = ValidatePart(updatedPart);

            if (validationErrors.Length > 0)
            {
                // Return 400 Bad Request with the validation errors
                return BadRequest(new { Errors = validationErrors });
            }

            // Find and return the part from the database
            var existingPart = await db.Parts.FindAsync(id);
            if (existingPart == null)
            {
                return NotFound();
            }

            // Update the part properties
            existingPart.Name = updatedPart.Name;
            existingPart.Price = updatedPart.Price;

            // Save the changes to the database
            db.Parts.Update(existingPart);
            await db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<PartsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Find and return the part from the database
            var partToDelete = await db.Parts.FindAsync(id);
            if (partToDelete == null)
            {
                return NotFound();
            }

            // Remove the part for the database
            db.Parts.Remove(partToDelete);
            // Save the changes to the database
            await db.SaveChangesAsync();
            // Return Status code 200
            return Ok();
        }

        private static string[] ValidatePart(Part part)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(part.Name))
            {
                errors.Add("Name is required.");
            }
            else if (part.Name.Length <= 2)
            {
                errors.Add("Name must be longer than 2 characters.");
            }

            if (part.Price <= 0)
            {
                errors.Add("Price must be a positive number.");
            }

            return errors.ToArray();
        }
    }
}
