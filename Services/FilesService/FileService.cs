using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Cooking_School.Core.ModelUsed;
using System.IO;

namespace Cooking_School.Services.FilesService
{
    public class FileService : IFileService
    {
        private readonly BlobContainerClient _filesContainer;
        private readonly string _key;
        private readonly string _StorageAccount;
        private readonly IConfiguration _configuration;
        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = _configuration["Azure:Key"];
            _StorageAccount = _configuration["Azure:StorageAccount"];
            var credential = new StorageSharedKeyCredential(_StorageAccount, _key);
            var blobUri = $"https://{_StorageAccount}.blob.core.windows.net";
            var blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
            _filesContainer = blobServiceClient.GetBlobContainerClient("files");
        }

        public async Task<List<BlobFile>> ListAsync()
        {
            List<BlobFile> files = new List<BlobFile>();
            await foreach (var file in _filesContainer.GetBlobsAsync())
            {
                string uri = _filesContainer.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new BlobFile()
                {
                    Uri = fullUri,
                    Name = name,
                    ContantType = file.Properties.ContentType
                });
            }

            return files;
        }


        public async Task<BlobResponse> UploadAsync(IFormFile blob)
        {
            BlobResponse response = new();

            // Generate a unique key using Guid
            string uniqueKey = Guid.NewGuid().ToString();

            // Concatenate the unique key with the original filename
            string fileName = $"{uniqueKey}_{blob.FileName}";

            BlobClient client = _filesContainer.GetBlobClient(fileName);

            await using (Stream? data = blob.OpenReadStream())
            {
                try
                {
                    await client.UploadAsync(data, overwrite: true);

                    response.Status = $"File {fileName} Uploaded Successfully";
                    response.error = false;
                    response.Blob.Uri = client.Uri.AbsoluteUri;
                    response.Blob.Name = client.Name;
                }
                catch (RequestFailedException ex) when (ex.Status == 409) // BlobAlreadyExists
                {
                    response.Status = $"File {fileName} already exists.";
                    response.error = true;
                    // Handle the duplicate upload case as needed
                }
            }

            return response;
        }

        public async Task<BlobFile?> DownloadAsync(string blobFileName)
        {
            BlobClient file = _filesContainer.GetBlobClient(blobFileName);

            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobContent = data;

                var content = await file.DownloadContentAsync();

                string name = blobFileName;
                string contentType = content.Value.Details.ContentType;
                return new BlobFile() { Contant = blobContent, Name = name, ContantType = contentType };
            }
            return null;
        }

        public async Task<BlobResponse> DeleteBlob(string blobFileName)
        {
            BlobClient file = _filesContainer.GetBlobClient(blobFileName);

            await file.DeleteAsync();

            return new BlobResponse()
            {
                error = false,
                Status = $"File: {blobFileName} has been successfully deleted"
            };
        }
    }
}
