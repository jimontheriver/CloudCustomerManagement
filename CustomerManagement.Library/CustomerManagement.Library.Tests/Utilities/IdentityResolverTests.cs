using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Library.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerManagement.Library.Tests.Utilities
{
    [TestClass]
    public class IdentityResolverTests
    {
        [TestMethod]
        public async Task Resolve_Resolves()
        {
            // arrange
            IIdentityResolver instanceUnderTest = new IdentityResolver();

            // act
            var result = await instanceUnderTest.GetUserNameAsync();

            // assert
            Assert.AreEqual("bob", result);
        }

}
}
