using Cooking_School_ASP.NET.Models;
using System.Net;

namespace Cooking_School_ASP.NET.Dtos.CourseDto
{
    public class ResponsCourseDto
    {
        public CourseDTO? CourseDTO { get; set; }
        public IList<CourseDTO>? Courses { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public Exception? Exception { get; set; }
    }
}
