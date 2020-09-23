using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElibForumMVC.Data
{
   public interface IUpload
    {

        CloudBlobContainer GetBlobContainer(string connectionString);

    }
}
