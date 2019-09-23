using CustomerManagement.Library.Models;
using System.Threading.Tasks;

namespace CustomerManagement.Library.UseCases
{
    public interface IDeleteCustomer
    {
        Task<SaveCustomerResponse> DeleteAsync(long id);
    }
}
