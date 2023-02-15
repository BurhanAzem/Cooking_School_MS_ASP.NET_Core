using System.Net;

namespace Cooking_School_ASP.NET.Dtos.AdminDto
{
    public class ResponsAdminDto 
    {
        public AdminDTO? AdminDTO { get; set; }
        public Exception? Exception { get; set; }
        public HttpStatusCode? StatusCode { get; set; } 
    }
}
