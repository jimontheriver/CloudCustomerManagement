using CustomerManagement.Library.Entities;
using CustomerManagement.Library.Models;
using CustomerManagement.Library.Repositories;
using CustomerManagement.Library.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Library.Utilities;

namespace CustomerManagement.Library.Tests.UseCases
{
    [TestClass]
    public class AddCustomerTests
    {
        [TestMethod]
        public async Task AddAsync_Delegates_To_Repository()
        {
            // arrange
            var scope = new DefaultScope();
            const long expectedId = 12L;
            SaveCustomerRequest request = new SaveCustomerRequest
            {
                IndustryCodes = new List<string> { "234" },
                Name = "notacustomer",
                SourceId = 3
            };

            scope.CustomerRepositoryMock.Setup(x => x.AddAsync(It.Is<Customer>(
                c => c.IndustryCodes.First() == request.IndustryCodes.First()
                && c.Name == request.Name
                && c.SourceId == request.SourceId
                ))).ReturnsAsync(expectedId);

            // act
            var result = await scope.InstanceUnderTest.AddAsync(request);

            // assert
            Assert.AreEqual(expectedId, result.Id);
        }

        private class DefaultScope
        {
            public IAddCustomer InstanceUnderTest { get; }
            public Mock<ICustomerRepository> CustomerRepositoryMock { get; }
            public Mock<IIdentityResolver> IdentityResolverMock { get; }

            public DefaultScope()
            {
                CustomerRepositoryMock = new Mock<ICustomerRepository>();
                IdentityResolverMock = new Mock<IIdentityResolver>();
                IdentityResolverMock.Setup(x => x.GetUserNameAsync()).ReturnsAsync("aname");
                InstanceUnderTest = new AddCustomer(CustomerRepositoryMock.Object, IdentityResolverMock.Object);
            }
        }
    }
}
