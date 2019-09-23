using Google.Cloud.Datastore.V1;

namespace CustomerManagement.Library.Repositories
{
    public interface IDatastoreManager
    {
        DatastoreDb GetDatastore();
    }
}