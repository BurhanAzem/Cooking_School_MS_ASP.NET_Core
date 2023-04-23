using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services.FilesService
{
    public interface IFileService
    {
        public Task<List<BlobFile>> ListAsync();
        public Task<BlobResponse> UploadAsync(IFormFile blob);
        public Task<BlobFile?> DownloadAsync(string blobFileName);
        public Task<BlobResponse> DeleteBlob(string blobFileName);

    }
}
