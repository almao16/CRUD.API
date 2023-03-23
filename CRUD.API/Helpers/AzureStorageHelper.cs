using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using static CRUD.API.BL.Utils.Enums;

namespace CRUD.API.Helpers
{
    public static class AzureStorageHelper
    {



        public static async Task<string> UploadFileAsync(string connection, IFormFile file, string fileIdentifier, BlobContainer container, string resource)
        {

            string retValue = string.Empty;
            string fileName = fileIdentifier.ToLower() + Path.GetExtension(file.FileName.ToLower());

            string containerName = container.ToString().ToLower();

            BlobContainerClient containerClient = new BlobContainerClient(connection, containerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);


            using (var memoryStream = file.OpenReadStream())
            {
                if (await blobClient.UploadAsync(memoryStream, true) != null)
                {

                    BlobSasBuilder blobSasBuilder = new BlobSasBuilder
                    {
                        StartsOn = DateTime.UtcNow,
                        ExpiresOn = DateTime.UtcNow.AddYears(100),
                        BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                        BlobName = blobClient.Name,
                        Resource = resource
                    };
                    blobSasBuilder.SetPermissions(BlobSasPermissions.Read);

                    retValue = blobClient.GenerateSasUri(blobSasBuilder).ToString();
                }

            }

            return retValue;
        }
        public static async Task<bool> DeleteFileAsync(string connection, BlobContainer container, string fileName)
        {
            string containerName = string.Empty;

            switch (container)
            {
                case BlobContainer.ATTACHMENTS:
                    containerName = BlobContainer.ATTACHMENTS.ToString().ToLower();
                    break;
            }

            BlobContainerClient containerClient = new BlobContainerClient(connection, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName.ToLower());

            return await blobClient.DeleteIfExistsAsync();
        }

        public static async Task<string> GetFileAsync(string connection, BlobContainer container, string fileName)
        {
            string retValue = string.Empty;
            string containerName = string.Empty;

            switch (container)
            {
                case BlobContainer.ATTACHMENTS:
                    containerName = BlobContainer.ATTACHMENTS.ToString().ToLower();
                    break;
            }

            BlobContainerClient containerClient = new BlobContainerClient(connection, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName.ToLower());

            if (await blobClient.ExistsAsync())
            {
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                MemoryStream fileStream = new MemoryStream();
                await download.Content.CopyToAsync(fileStream);
                retValue = Convert.ToBase64String(fileStream.ToArray());
            }

            return retValue;
        }

        public static async Task VerifyAzureContainersAsync(string connection)
        {
            BlobContainerClient containerAttachments = new BlobContainerClient(connection, BlobContainer.ATTACHMENTS.ToString().ToLower());
            await containerAttachments.CreateIfNotExistsAsync();

            BlobContainerClient containerDocuments = new BlobContainerClient(connection, BlobContainer.DOCUMENTS.ToString().ToLower());
            await containerDocuments.CreateIfNotExistsAsync();

        }
    }

}
