namespace AzureBlob.Services
{
    public interface IContainerService
    {
        Task<bool> CreateContainer(string containerName);
        Task<bool> DeleteContainerByName(string name);
        Task<IEnumerable<string>> GetAllContainer();
        Task<bool> CopyContainerFromAnother(string sourceContainerName, string destinationContainerName);
    }
}