using CustomerManagement.Library.Entities;
using CustomerManagement.Library.Models;
using CustomerManagement.Library.Repositories;
using CustomerManagement.Library.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Library.Tests.UseCases
{
    [TestClass]
    public class DeleteCustomerTests
    {
        [TestMethod]
        public async Task DeleteAsync_Delegates_To_Repository()
        {
            // arrange
            var scope = new DefaultScope();
            const long expectedId = 12L;
            Customer customer = new Customer
            {
                Id = expectedId,
                Name = "notacustomer",
                SourceId = 12311,
                IndustryCodes = new List<string> { "12e1" }
            };

            scope.CustomerRepositoryMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(
                customer);

                scope.CustomerRepositoryMock.Setup(x => x.DeleteAsync(expectedId)).ReturnsAsync(
                expectedId);

            // act
            var result = await scope.InstanceUnderTest.DeleteAsync(expectedId);

            // assert
            Assert.AreEqual(customer.Id, result.Id);
        }

        private class DefaultScope
        {
            public IDeleteCustomer InstanceUnderTest { get; }
            public Mock<ICustomerRepository> CustomerRepositoryMock { get; }

            public DefaultScope()
            {
                CustomerRepositoryMock = new Mock<ICustomerRepository>();
                InstanceUnderTest = new DeleteCustomer(CustomerRepositoryMock.Object);
            }
        }
    }
}
