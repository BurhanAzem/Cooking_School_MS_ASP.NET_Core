using Cooking_School_ASP.NET.Dtos.CookClassDto;
using System.Net;

namespace Cooking_School_ASP.NET.Dtos.ProjectDto
{
    public class ResponsProjectDto
    {
        public ProjectDTO? projectDto { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public Exception? Exception { get; set; }
    }
}
