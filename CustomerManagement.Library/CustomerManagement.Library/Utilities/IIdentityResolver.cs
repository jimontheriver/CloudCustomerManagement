using System.Threading.Tasks;

namespace CustomerManagement.Library.Utilities
{
    public interface IIdentityResolver
    {
        Task<string> GetUserNameAsync();
    }
}
