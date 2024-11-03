using MongoDBIntegration.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBIntegration.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(string id);
        Task CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(string id, Customer customer);
        Task<bool> DeleteCustomerAsync(string id);
    }
}
