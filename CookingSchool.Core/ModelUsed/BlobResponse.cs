namespace Cooking_School.Core.ModelUsed
{
    public class BlobResponse
    {
        public BlobResponse() 
        {
            Blob = new BlobFile();
        }
        public string? Status { get; set; }
        public bool? error { get; set; }
        public BlobFile Blob { get; set; }
    }
}
