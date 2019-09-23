namespace CustomerManagement.Library.Repositories
{
    public interface IRepositoryConfiguration
    {
        string Namespace { get; set; }
        string ProjectId { get; set; }
    }
}