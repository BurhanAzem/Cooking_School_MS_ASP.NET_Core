using Azure.Storage;
using Azure.Storage.Blobs;
using Cooking_School_ASP.NET.ModelUsed;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;

namespace Cooking_School_ASP.NET.Services.FilesService
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
            BlobClient client = _filesContainer.GetBlobClient(blob.FileName);
            
            await using (Stream? data = blob.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            response.Status = $"File {blob.FileName} Uploaded Sucessfully";
            response.error = false;
            response.Blob.Uri = client.Uri.AbsoluteUri;
            response.Blob.Name = client.Name;

            return response;    
        }

        public async Task<BlobFile?> DownloadAsync(string blobFileName)
        {
            BlobClient file = _filesContainer.GetBlobClient(blobFileName);
            
            if(await file.ExistsAsync())
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
