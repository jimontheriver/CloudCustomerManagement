using System;
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
    public class UpdateCustomerTests
    {
        [TestMethod]
        public async Task UpdateAsync_Delegates_To_Repository()
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

            scope.CustomerRepositoryMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(new Customer { Id = expectedId });

            scope.CustomerRepositoryMock.Setup(x => x.UpdateAsync(It.Is<Customer>(
                c => c.Id == expectedId
                && c.IndustryCodes.First() == request.IndustryCodes.First()
                && c.Name == request.Name
                && c.SourceId == request.SourceId
                ))).ReturnsAsync(expectedId);

            // act
            var result = await scope.InstanceUnderTest.UpdateAsync(expectedId, request);

            // assert
            Assert.AreEqual(expectedId, result.Id);
        }

        [TestMethod]
        public async Task UpdateAsync_Sets_Updated_Audit_Columns()
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

            scope.CustomerRepositoryMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(new Customer { Id = expectedId, Updated = DateTimeOffset.MinValue, UpdatedBy = "franklin"});

            scope.CustomerRepositoryMock.Setup(x => x.UpdateAsync(It.Is<Customer>(
                c => c.Updated > DateTimeOffset.MinValue
                     && c.UpdatedBy == DefaultScope.UserName
            ))).ReturnsAsync(expectedId);

            // act
            var result = await scope.InstanceUnderTest.UpdateAsync(expectedId, request);

            // assert
            Assert.AreEqual(expectedId, result.Id);
        }

        [TestMethod]
        public async Task UpdateAsync_Throws_NotFound_If_Doesnt_Exist()
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

            scope.CustomerRepositoryMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(null as Customer);

            // act and assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(() => scope.InstanceUnderTest.UpdateAsync(expectedId, request));
        }

        [TestMethod]
        public async Task UpdateAsync_Throws_Gone_If_Deleted()
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

            scope.CustomerRepositoryMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(new Customer { IsDeleted = true});

            // act and assert
            await Assert.ThrowsExceptionAsync<GoneException>(() => scope.InstanceUnderTest.UpdateAsync(expectedId, request));
        }

        private class DefaultScope
        {
            public const string UserName = "aname";
            public IUpdateCustomer InstanceUnderTest { get; }
            public Mock<ICustomerRepository> CustomerRepositoryMock { get; }
            public Mock<IIdentityResolver> IdentityResolverMock { get; }

            public DefaultScope()
            {
                CustomerRepositoryMock = new Mock<ICustomerRepository>();
                IdentityResolverMock = new Mock<IIdentityResolver>();
                IdentityResolverMock.Setup(x => x.GetUserNameAsync()).ReturnsAsync(UserName);
                InstanceUnderTest = new UpdateCustomer(CustomerRepositoryMock.Object, IdentityResolverMock.Object);
            }
        }
    }
}
