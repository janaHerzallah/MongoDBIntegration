using Microsoft.AspNetCore.Mvc;
using MongoDBIntegration.Entities;
using MongoDBIntegration.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _customerService.GetAllCustomersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(string id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            await _customerService.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Customer customer)
        {
            var success = await _customerService.UpdateCustomerAsync(id, customer);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _customerService.DeleteCustomerAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
