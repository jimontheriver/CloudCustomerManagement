using CustomerManagement.Library.Models;
using System.Threading.Tasks;

namespace CustomerManagement.Library.UseCases
{
    public interface IGetCustomer
    {
        Task<GetCustomerResponse> GetAsync(long id);
    }
}
