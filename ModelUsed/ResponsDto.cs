using Cooking_School_ASP.NET.Dtos.AdminDto;
using Cooking_School_ASP.NET.Dtos.CourseDto;
using System.Net;

namespace Cooking_School_ASP.NET.ModelUsed
{
    public class ResponsDto<T> where T : class
    {
        public T? Dto { get; set; }
        public IList<T>? ListDto { get; set; }
        public Exception? Exception { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        
    }
}
