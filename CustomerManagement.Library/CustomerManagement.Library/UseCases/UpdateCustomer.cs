using CustomerManagement.Library.Models;
using CustomerManagement.Library.Repositories;
using System;
using System.Threading.Tasks;
using CustomerManagement.Library.Utilities;

namespace CustomerManagement.Library.UseCases
{
    public class UpdateCustomer : IUpdateCustomer
    {
        private readonly ICustomerRepository repository;
        private readonly IIdentityResolver identityResolver;

        public UpdateCustomer(ICustomerRepository repository, IIdentityResolver identityResolver)
        {
            this.repository = repository;
            this.identityResolver = identityResolver;
        }

        public async Task<SaveCustomerResponse> UpdateAsync(long id, SaveCustomerRequest request)
        {
            var existing = await repository.GetAsync(id);
            if (existing  == null)
            {
                throw new NotFoundException();
            }

            if (existing.IsDeleted)
            {
                throw new GoneException();
            }

            Entities.Customer customer = new Entities.Customer
            {
                Id = id,
                Name = request.Name,
                SourceId = request.SourceId,
                IndustryCodes = request.IndustryCodes,
                UpdatedBy = await identityResolver.GetUserNameAsync(),
                Updated = DateTime.UtcNow,
                Created = existing.Created,
                CreatedBy = existing.CreatedBy
            };

            var resultId = await repository.UpdateAsync(customer);

            return new SaveCustomerResponse
            {
                Id = resultId
            };
        }
    }
}
