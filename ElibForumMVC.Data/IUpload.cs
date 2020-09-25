using Microsoft.WindowsAzure.Storage.Blob;

namespace ElibForumMVC.Data
{
    public interface IUpload
    {

        CloudBlobContainer GetBlobContainer(string connectionString);

    }
}
