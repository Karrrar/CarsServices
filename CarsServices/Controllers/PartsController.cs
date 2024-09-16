using CarsServices.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private CarsContaxt db;

        public PartsController()
        {
            db = new CarsContaxt();
        }

        // GET: api/<PartsController>
        [HttpGet]
        public List<Part> Get()
        {
            return db.Parts.ToList();
        }

        [HttpGet("PartsPrices")]
        public List<string> GetPartsPrices()
        {
            return db.Parts
                .Where(s => s.Price > 200)
                .Where(s => s.Price < 300)
                .Select(x => x.Name)
                .ToList();
        }

        [HttpGet("PartsNames")]
        public List<string> GetPartsNames()
        {
            return db.Parts
                .Select(x => x.Name)
                .ToList();
        }

        // GET api/<PartsController>/5
        [HttpGet("{id}")]
        public Part Get(int id)
        {
            return db.Parts.Where(c => c.PartId == id).FirstOrDefault();
        }

        // POST api/<PartsController>
        [HttpPost]
        public async Task Post([FromBody] Part value)
        {
            await db.Parts.AddAsync(value);
            await db.SaveChangesAsync();
        }

        // PUT api/<PartsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Part value)
        {
            var partToUpdate = db.Parts.Where(c => c.PartId == id).FirstOrDefault();
            
            partToUpdate.Name = value.Name;
            partToUpdate.Price = value.Price;

            db.SaveChanges();
        }

        // DELETE api/<PartsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var partToDelete = db.Parts.Where(c => c.PartId == id).FirstOrDefault();
            db.Parts.Remove(partToDelete);

            db.SaveChanges();
        }
    }
}
