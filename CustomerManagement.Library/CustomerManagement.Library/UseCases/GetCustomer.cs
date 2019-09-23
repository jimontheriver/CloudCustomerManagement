using CustomerManagement.Library.Models;
using CustomerManagement.Library.Repositories;
using System.Threading.Tasks;

namespace CustomerManagement.Library.UseCases
{
    public class GetCustomer : IGetCustomer
    {
        private readonly ICustomerRepository repository;

        public GetCustomer(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetCustomerResponse> GetAsync(long id)
        {
            var customer = await repository.GetAsync(id);
            return null == customer || customer.IsDeleted ? null :
             new GetCustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                SourceId = customer.SourceId,
                IndustryCodes = customer.IndustryCodes
            };
        }
    }
}
