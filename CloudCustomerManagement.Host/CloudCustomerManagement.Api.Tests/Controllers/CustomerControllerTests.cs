using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCustomerManagement.Api.Controllers;
using CustomerManagement.Library.Models;
using CustomerManagement.Library.UseCases;
using CustomerManagement.Library.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CloudCustomerManagement.Api.Tests.Controllers
{
    [TestClass]
    public class CustomerControllerTests
    {
        [TestMethod]
        public async Task AddCustomer_Returns_Location_If_Successful()
        {
            // arrange
            var scope = new DefaultScope();
            var expectedId = 12L;
            var saveCustomerRequest = new SaveCustomerRequest()
            {
                Name = "good enough"
            };
            var saveCustomerResponse = new SaveCustomerResponse
            {
                Id = expectedId
            };

            scope.AddCustomerMock.Setup(x => x.AddAsync(saveCustomerRequest)).ReturnsAsync(saveCustomerResponse);
            scope.SetupUrlHelper(expectedId);

            // act
            var result = await scope.InstanceUnderTest.PostAsync(saveCustomerRequest);

            // assert
            Assert.IsTrue(result.Value.Links.First().Href.EndsWith(expectedId.ToString()));
        }

        [TestMethod]
        public async Task GetCustomer_Returns_Customer_If_Found()
        {
            // arrange
            var scope = new DefaultScope();
            var expectedId = 12L;
            var getCustomerResponse = new GetCustomerResponse
            {
                Id = expectedId,
                SourceId = 123,
                IndustryCodes = new List<String> { "123123"},
                Name = "good enough"
            };
            scope.SetupUrlHelper(expectedId);
            scope.GetCustomerMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(
                getCustomerResponse);

            // act
            var result = await scope.InstanceUnderTest.GetAsync(expectedId);

            // assert
            Assert.AreEqual(getCustomerResponse.Name, result.Value.Name);
            Assert.AreEqual(getCustomerResponse.SourceId, result.Value.SourceId);
            Assert.AreEqual(getCustomerResponse.IndustryCodes.First(), result.Value.IndustryCodes.First());
        }

        [TestMethod]
        public async Task GetCustomer_Returns_Location_If_Found()
        {
            // arrange
            var scope = new DefaultScope();
            var expectedId = 12L;
            var getCustomerResponse = new GetCustomerResponse
            {
                Id = expectedId,
                Name = "good enough"
            };
            scope.SetupUrlHelper(expectedId);
            scope.GetCustomerMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(
                getCustomerResponse);

            // act
            var result = await scope.InstanceUnderTest.GetAsync(expectedId);

            // assert
            Assert.IsTrue(result.Value.Links.First().Href.EndsWith(expectedId.ToString()));
            Assert.AreEqual("self", result.Value.Links.First().Rel);
            Assert.AreEqual("GET", result.Value.Links.First().Method);
        }

        [TestMethod]
        public async Task GetCustomer_Returns_404_If_Not_Found()
        {
            // arrange
            var scope = new DefaultScope();
            var expectedId = 12L;
            scope.GetCustomerMock.Setup(x => x.GetAsync(expectedId)).ReturnsAsync(null as GetCustomerResponse);

            // act
            var result = await scope.InstanceUnderTest.GetAsync(expectedId);

            // assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateCustomer_Returns_Location_If_Successful()
        {
            // arrange
            var scope = new DefaultScope();
            var expectedId = 12L;
            var saveCustomerRequest = new SaveCustomerRequest()
            {
                Name = "good enough"
            };
            var saveCustomerResponse = new SaveCustomerResponse
            {
                Id = expectedId
            };

            scope.UpdateCustomerMock.Setup(x => x.UpdateAsync(expectedId, saveCustomerRequest)).ReturnsAsync(saveCustomerResponse);
            scope.SetupUrlHelper(expectedId);

            // act
            var result = await scope.InstanceUnderTest.PutAsync(expectedId, saveCustomerRequest);

            // assert
            Assert.IsTrue(result.Value.Links.First().Href.EndsWith(expectedId.ToString()));
            Assert.AreEqual("self", result.Value.Links.First().Rel);
            Assert.AreEqual("GET", result.Value.Links.First().Method);
        }

        [TestMethod]
        public async Task UpdateCustomer_Returns_404_If_NotFound()
        {
            // arrange
            var scope = new DefaultScope();
            var expectedId = 12L;
            var saveCustomerRequest = new SaveCustomerRequest()
            {
                Name = "good enough"
            };

            scope.UpdateCustomerMock.Setup(x => x.UpdateAsync(expectedId, saveCustomerRequest)).ThrowsAsync(new NotFoundException());
            
            // act
            var result = await scope.InstanceUnderTest.PutAsync(expectedId, saveCustomerRequest);

            // assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task UpdateCustomer_Returns_409_If_Deleted()
        {
            // arrange
            var scope = new DefaultScope();
            var expectedId = 12L;
            var saveCustomerRequest = new SaveCustomerRequest()
            {
                Name = "good enough"
            };

            scope.UpdateCustomerMock.Setup(x => x.UpdateAsync(expectedId, saveCustomerRequest)).ThrowsAsync(new GoneException());

            // actd
            var result = await scope.InstanceUnderTest.PutAsync(expectedId, saveCustomerRequest);

            // assert
            Assert.AreEqual(StatusCodes.Status410Gone, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task DeleteCustomer_Returns_Ok()
        {
            // arrange
            var scope = new DefaultScope();
            var expectedId = 12L;
           
            var saveCustomerResponse = new SaveCustomerResponse
            {
                Id = expectedId
            };

            scope.DeleteCustomerMock.Setup(x => x.DeleteAsync(expectedId)).ReturnsAsync(saveCustomerResponse);

            // act
            var result = await scope.InstanceUnderTest.DeleteAsync(expectedId);

            // assert
            Assert.IsInstanceOfType(result, typeof(AcceptedResult));
        }

        private class DefaultScope
        {
            public CustomerController InstanceUnderTest { get; }
            public Mock<IAddCustomer> AddCustomerMock { get; }
            public Mock<IGetCustomer> GetCustomerMock { get; }
            public Mock<IUpdateCustomer> UpdateCustomerMock { get; }
            public Mock<IDeleteCustomer> DeleteCustomerMock { get; }
            public Mock<IUrlHelper> UrlHelperMock { get; }

            public DefaultScope()
            {
                AddCustomerMock = new Mock<IAddCustomer>();
                GetCustomerMock = new Mock<IGetCustomer>();
                UpdateCustomerMock = new Mock<IUpdateCustomer>();
                DeleteCustomerMock = new Mock<IDeleteCustomer>();
                UrlHelperMock = new Mock<IUrlHelper>(MockBehavior.Strict);

                InstanceUnderTest = new CustomerController(AddCustomerMock.Object, UpdateCustomerMock.Object,
                    GetCustomerMock.Object, DeleteCustomerMock.Object) {Url = UrlHelperMock.Object};
            }

            public void SetupUrlHelper(long expectedId)
            {
                UrlHelperMock.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                    .Returns(expectedId.ToString);
            }
        }
    }
}
