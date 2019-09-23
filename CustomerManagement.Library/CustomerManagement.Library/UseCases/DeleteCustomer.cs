using CustomerManagement.Library.Models;
using CustomerManagement.Library.Repositories;
using System.Threading.Tasks;

namespace CustomerManagement.Library.UseCases
{
    public class DeleteCustomer : IDeleteCustomer
    {
        private readonly ICustomerRepository repository;

        public DeleteCustomer(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SaveCustomerResponse> DeleteAsync(long id)
        {
            var result = await repository.DeleteAsync(id);
            return new SaveCustomerResponse{ Id = result };
        }
    }
}
