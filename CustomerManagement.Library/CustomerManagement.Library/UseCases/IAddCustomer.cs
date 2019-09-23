using CustomerManagement.Library.Models;
using System.Threading.Tasks;

namespace CustomerManagement.Library.UseCases
{
    public interface IAddCustomer
    {
        Task<SaveCustomerResponse> AddAsync(SaveCustomerRequest request);
    }
}
