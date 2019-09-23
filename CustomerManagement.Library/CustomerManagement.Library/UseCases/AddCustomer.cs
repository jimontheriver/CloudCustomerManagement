using CustomerManagement.Library.Models;
using CustomerManagement.Library.Repositories;
using System;
using System.Threading.Tasks;
using CustomerManagement.Library.Utilities;

namespace CustomerManagement.Library.UseCases
{
    public class AddCustomer : IAddCustomer
    {
        private readonly ICustomerRepository repository;
        private readonly IIdentityResolver identityResolver;

        public AddCustomer(ICustomerRepository repository, IIdentityResolver identityResolver)
        {
            this.repository = repository;
            this.identityResolver = identityResolver;
        }

        public async Task<SaveCustomerResponse> AddAsync(SaveCustomerRequest request)
        {
            Entities.Customer customer = new Entities.Customer
            {
                Name = request.Name,
                SourceId = request.SourceId,
                IndustryCodes = request.IndustryCodes,
                CreatedBy = await identityResolver.GetUserNameAsync(),
                Created = DateTime.UtcNow
            };
            customer.Updated = customer.Created;
            customer.UpdatedBy = customer.CreatedBy;

            var id = await repository.AddAsync(customer);

            return new SaveCustomerResponse
            {
                Id = id
            };
        }
    }
}
