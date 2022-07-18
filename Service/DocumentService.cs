using Azure.Storage;
using Azure.Storage.Blobs;
using GoogleMaps.LocationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Model.Exceptions;
using Provider.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DocumentService
    {
        private readonly IConfiguration config;

        public DocumentService(IConfiguration config) {
            this.config = config;
        }

        public async Task<string> UploadSharedFile(Stream fileStream, string fileName)
        {
            var accountName = config["Azure:Shared:AccountName"];
            var imageContainer = config["Azure:Shared:ImageContainer"];
            var accountKey = config["Azure:Shared:AccountKey"];

            // Create StorageSharedKeyCredentials object by reading
            // the values from the configuration (appsettings.json)
            StorageSharedKeyCredential storageCredentials = new StorageSharedKeyCredential(accountName, accountKey);

            var isExist = true;
            var i = 0;
            var fileSplitName = fileName.Split(".");
            var newFileName = fileSplitName[0] + fileSplitName[1];

            // Create a URI to the blob
            Uri blobUri = new Uri("https://" + accountName + ".blob.core.windows.net/" + imageContainer + "/" + newFileName);
            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            while (isExist)
            {
                newFileName = $@"{fileSplitName[0]}{i}.{fileSplitName[1]}";

                blobUri = new Uri("https://" + accountName + ".blob.core.windows.net/" + imageContainer + "/" + newFileName);
                blobClient = new BlobClient(blobUri, storageCredentials);

                if (blobClient.Exists())
                {
                    i++;
                }
                else
                {
                    isExist = false;
                }

            }
            // Upload the file
            await blobClient.UploadAsync(fileStream);

            return @$"https://{accountName}.blob.core.windows.net/{imageContainer}/{newFileName}";
        }

        public bool DeleteSharedDocumentByUrl(string documentUrl)
        {
            if (documentUrl == null || string.IsNullOrEmpty(documentUrl))
                throw new ArgumentNullException();


            var accountName = config["Azure:Shared:AccountName"];
            var accountKey = config["Azure:Shared:AccountKey"];

            // Create StorageSharedKeyCredentials object by reading
            // the values from the configuration (appsettings.json)
            StorageSharedKeyCredential storageCredentials = new StorageSharedKeyCredential(accountName, accountKey);

            // Create a URI to the blob
            Uri blobUri = new Uri(documentUrl);
            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            if (blobClient.Exists())
            {
                blobClient.Delete();
                return true;
            }

            return false;
        }
    }
}
