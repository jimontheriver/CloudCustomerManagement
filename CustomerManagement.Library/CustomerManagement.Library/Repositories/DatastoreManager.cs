using Google.Cloud.Datastore.V1;

namespace CustomerManagement.Library.Repositories
{
    public class DatastoreManager : IDatastoreManager
    {
        private IRepositoryConfiguration _repositoryConfiguration;

        public DatastoreManager(IRepositoryConfiguration repositoryConfiguration)
        {
            _repositoryConfiguration = repositoryConfiguration;
        }

        public DatastoreDb GetDatastore()
        {
            DatastoreDb db = DatastoreDb.Create(_repositoryConfiguration.ProjectId, _repositoryConfiguration.Namespace);
            return db;
        }
    }
}