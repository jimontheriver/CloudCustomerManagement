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
    public class GetCustomerTests
    {
        [TestMethod]
        public async Task GetAsync_Delegates_To_Repository()
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

            // act
            var result = await scope.InstanceUnderTest.GetAsync(expectedId);

            // assert
            Assert.AreEqual(customer.Id, result.Id);
            Assert.AreEqual(customer.Name, result.Name);
            Assert.AreEqual(customer.SourceId, result.SourceId);
            Assert.AreEqual(customer.IndustryCodes.First(), result.IndustryCodes.First());
        }

        [TestMethod]
        public async Task GetAsync_IsDeleted_Response_Returns_Null()
        {
            // arrange
            var scope = new DefaultScope();
            const long expectedId = 12L;
            Customer customer = new Customer
            {
                Id = expectedId,
                Name = "notacustomer",
                SourceId = 12311,
                IndustryCodes = new List<string> { "12e1" },
                IsDeleted = true
            };

            scope.CustomerRepositoryMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(
                customer);

            // act
            var result = await scope.InstanceUnderTest.GetAsync(expectedId);

            // assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAsync_Null_Response_Returns_Null()
        {
            // arrange
            var scope = new DefaultScope();
            const long expectedId = 12L;

            scope.CustomerRepositoryMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(
                null as Customer);

            // act
            var result = await scope.InstanceUnderTest.GetAsync(expectedId);

            // assert
            Assert.IsNull(result);
        }

        private class DefaultScope
        {
            public IGetCustomer InstanceUnderTest { get; }
            public Mock<ICustomerRepository> CustomerRepositoryMock { get; }

            public DefaultScope()
            {
                CustomerRepositoryMock = new Mock<ICustomerRepository>();
                InstanceUnderTest = new GetCustomer(CustomerRepositoryMock.Object);
            }
        }
    }
}
