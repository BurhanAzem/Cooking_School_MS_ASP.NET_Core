using System.Net;

namespace Cooking_School_ASP.NET.Dtos.ChefDto
{
    public class ResponsChefDto
    {
        public ChefDTO? ChefDto { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public Exception? Exception { get; set; }
    }
}
