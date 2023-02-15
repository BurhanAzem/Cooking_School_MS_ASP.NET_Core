using Cooking_School_ASP.NET.Dtos.CookClassDto;
using System.Net;

namespace Cooking_School_ASP.NET.Dtos.ApplicationDto
{
    public class ResponsApplicationDto
    {
        public ApplicationDTO? ApplicationDto { get; set; }
        public IList<ApplicationDTO>? applicationDTOs { get; set; }
        public HttpStatusCode? StatusCode { get; set; } 
        public Exception? Exception { get; set; }   
    }
}
