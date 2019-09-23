using CustomerManagement.Library.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Library.Repositories
{
    public interface ICustomerRepository
    {
        Task<long> AddAsync(Customer customer);
        Task<long> UpdateAsync(Customer customer);
        Task<Customer> GetAsync(long id);
        Task<long> DeleteAsync(long id);
    }
}
