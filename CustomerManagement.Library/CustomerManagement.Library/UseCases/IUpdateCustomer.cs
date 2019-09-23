using CustomerManagement.Library.Models;
using System.Threading.Tasks;

namespace CustomerManagement.Library.UseCases
{
    public interface IUpdateCustomer
    {
        Task<SaveCustomerResponse> UpdateAsync(long id, SaveCustomerRequest request);
    }
}
