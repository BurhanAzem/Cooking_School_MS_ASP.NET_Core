using System.Net;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class ResponsCookClass
    {
        public CookClassDTO? CookClassDto { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public Exception? Exception { get; set; }
    }
}
