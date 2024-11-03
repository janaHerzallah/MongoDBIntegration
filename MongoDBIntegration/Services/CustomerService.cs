using MongoDB.Driver;
using MongoDBIntegration.Data;
using MongoDBIntegration.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBIntegration.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(MongoDbService mongoDBService)
        {
            _customers = mongoDBService.Database.GetCollection<Customer>("Customers");
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customers.Find(customer => true).ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            return await _customers.Find(customer => customer.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _customers.InsertOneAsync(customer);
        }

        public async Task<bool> UpdateCustomerAsync(string id, Customer customerIn)
        {
            var result = await _customers.ReplaceOneAsync(customer => customer.Id == id, customerIn);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var result = await _customers.DeleteOneAsync(customer => customer.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
