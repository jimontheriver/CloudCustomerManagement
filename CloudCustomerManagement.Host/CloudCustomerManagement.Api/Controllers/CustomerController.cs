using System.Collections.Generic;
using System.Threading.Tasks;
using CloudCustomerManagement.Api.Models;
using CustomerManagement.Library.Models;
using CustomerManagement.Library.UseCases;
using CustomerManagement.Library.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Link = CloudCustomerManagement.Api.Models.Link;

namespace CloudCustomerManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAddCustomer addCustomer;
        private readonly IDeleteCustomer deleteCustomer;
        private readonly IGetCustomer getCustomer;
        private readonly IUpdateCustomer updateCustomer;

        public CustomerController(IAddCustomer addCustomer, IUpdateCustomer updateCustomer, IGetCustomer getCustomer,
            IDeleteCustomer deleteCustomer)
        {
            this.addCustomer = addCustomer;
            this.updateCustomer = updateCustomer;
            this.getCustomer = getCustomer;
            this.deleteCustomer = deleteCustomer;
        }

        //// GET: api/Customer
        //[HttpGet]
        //public IEnumerable<string> Get(int? pageNum = 1, int? pageSize = 20)
        //{
        //TODO - set search params, etc
        //}

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<GetCustomerLinkedResponse>> GetAsync(long id)
        {
            var customer = await getCustomer.GetAsync(id);
            if (null == customer) return NotFound();
            return CreateGetCustomerLinkedResponse(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<SaveCustomerLinkedResponse>> PostAsync([FromBody] SaveCustomerRequest value)
        {
            var response = await addCustomer.AddAsync(value);
            var result = CreateSaveCustomerLinkedResponse(response);
            return result;
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SaveCustomerLinkedResponse>> PutAsync(long id, [FromBody] SaveCustomerRequest value)
        {
            try
            {
                var response = await updateCustomer.UpdateAsync(id, value); var result = CreateSaveCustomerLinkedResponse(response);
                return result;
            }
            catch (GoneException)
            {
                return StatusCode(StatusCodes.Status410Gone);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            await deleteCustomer.DeleteAsync(id);
            return Accepted();
        }

        private GetCustomerLinkedResponse CreateGetCustomerLinkedResponse(GetCustomerResponse response)
        {
            var result = new GetCustomerLinkedResponse
            {
                SourceId = response.SourceId,
                Name = response.Name,
                IndustryCodes = response.IndustryCodes,
                Links = new List<Link> { BuildLinkFromId(response.Id) }
            };
            return result;
        }


        private SaveCustomerLinkedResponse CreateSaveCustomerLinkedResponse(SaveCustomerResponse response)
        {
            var result = new SaveCustomerLinkedResponse
            {
                Links = new List<Link> { BuildLinkFromId(response.Id)}
            };
            return result;
        }


        private string BuildHrefFromId(long id)
        {
            return Url.Action("GetAsync", "Customer", new { Id = id });
        }

        private Link BuildLinkFromId(long id)
        {
            return new Link { Rel = "self", Method = "GET", Href = BuildHrefFromId(id)};
        }
    }
}