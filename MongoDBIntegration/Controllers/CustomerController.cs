using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDBIntegration.Data;
using MongoDBIntegration.Entities;
using MongoDB.Driver; 

namespace MongoDBIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IMongoCollection<Customer> _customers;

        public CustomerController(MongoDbService mongoDBService)
        {
            _customers = mongoDBService.Database.GetCollection<Customer>("Customers");
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get ()
        {
            return await _customers.Find(customer => true).ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(string id)
        {
            var customer = await _customers.Find<Customer>(customer => customer.Id == id).FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            await _customers.InsertOneAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Customer customerIn)
        {
            var customer = await _customers.Find<Customer>(customer => customer.Id == id).FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            await _customers.ReplaceOneAsync(customer => customer.Id == id, customerIn);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _customers.Find<Customer>(customer => customer.Id == id).FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            await _customers.DeleteOneAsync(customer => customer.Id == id);
            return Ok();
        }


    }
}
