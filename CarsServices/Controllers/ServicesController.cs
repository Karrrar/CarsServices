using CarsServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsServices.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ServicesController : ControllerBase
  {
    private CarsContaxt db;

    public ServicesController(CarsContaxt carsContaxt)
    {
      db = carsContaxt;
    }

    // GET: api/<ServicesController>
    [HttpGet]
    public async Task<List<Service>> GetServices()
    {
      // Retrieve all services, including the associated car and parts used
      return await db.Services
                     .Include(s => s.Car)
                     .Include(s => s.PartsUsed)
                     .ToListAsync();
    }

    // GET api/<ServicesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Service>> GetService(int id)
    {
      // Retrieve a specific service by ID, including the car and parts used
      var service = await db.Services
                            .Include(s => s.Car)
                            .Include(s => s.PartsUsed)
                            .FirstOrDefaultAsync(s => s.ServiceId == id);

      if (service == null)
      {
        return NotFound();
      }

      return service;
    }

    // POST api/<ServicesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Service newService)
    {
      // Validate the newService object
      var validationErrors = ValidateService(newService);

      if (validationErrors.Length > 0)
      {
        // Return 400 Bad Request with the validation errors
        return BadRequest(new { Errors = validationErrors });
      }

      // Retrieve the existing parts from the database using PartIds from the request
      var partIds = newService.PartsUsed.Select(p => p.PartId).ToList();
      var partsFromDb = await db.Parts.Where(p => partIds.Contains(p.PartId)).ToListAsync();

      if (partsFromDb.Count != partIds.Count)
      {
        return BadRequest("One or more PartIds are invalid.");
      }

      // Associate the existing parts with the new service
      newService.PartsUsed = partsFromDb;


      // Add the new service to the database
      await db.Services.AddAsync(newService);
      await db.SaveChangesAsync();

      // Return 201 Created with the newly created service
      return CreatedAtAction(nameof(GetService), new { id = newService.ServiceId }, newService);
    }

    // PUT api/<ServicesController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Service updatedService)
    {
      // Validate the updatedService object
      var validationErrors = ValidateService(updatedService);
      if (validationErrors.Length > 0)
      {
        return BadRequest(new { Errors = validationErrors });
      }

      // Find the existing service by its ID
      var existingService = await db.Services
                                    .Include(s => s.PartsUsed) // Include PartsUsed to clear existing relations
                                    .FirstOrDefaultAsync(s => s.ServiceId == id);
      if (existingService == null)
      {
        return NotFound();
      }

      // Retrieve the existing parts from the database using PartIds from the request
      var partIds = updatedService.PartsUsed.Select(p => p.PartId).ToList();
      var partsFromDb = await db.Parts.Where(p => partIds.Contains(p.PartId)).ToListAsync();

      existingService.PartsUsed.Clear();

      if (partsFromDb.Count != partIds.Count)
      {
        return BadRequest("One or more PartIds are invalid.");
      }

      // Update the service properties
      existingService.Date = updatedService.Date;
      existingService.Description = updatedService.Description;
      existingService.CarId = updatedService.CarId;
      // Update with parts fetched from DB
      existingService.PartsUsed = partsFromDb;


      db.Services.Update(existingService);
      await db.SaveChangesAsync();

      return NoContent();
    }

    // DELETE api/<ServicesController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var serviceToDelete = await db.Services.FindAsync(id);
      if (serviceToDelete == null)
      {
        return NotFound();
      }

      db.Services.Remove(serviceToDelete);
      await db.SaveChangesAsync();

      return Ok();
    }

    private static string[] ValidateService(Service service)
    {
      List<string> errors = new List<string>();

      if (service.CarId <= 0)
      {
        errors.Add("CarId must be a positive number.");
      }

      if (string.IsNullOrEmpty(service.Description))
      {
        errors.Add("Description is required.");
      }

      if (service.Date == default)
      {
        errors.Add("A valid Date is required.");
      }

      return errors.ToArray();
    }
  }
}
