using System.Threading.Tasks;

namespace CustomerManagement.Library.Utilities
{
    public class IdentityResolver : IIdentityResolver
    {
        public Task<string> GetUserNameAsync()
        {
            return Task.FromResult("bob");
        }
    }
}
