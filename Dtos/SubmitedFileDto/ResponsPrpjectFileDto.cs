using Cooking_School_ASP.NET.Dtos.CookClassDto;
using System.Net;

namespace Cooking_School_ASP.NET.Dtos.ProjectFileDto
{
    public class ResponsPrpjectFileDto
    {
        public SubmitedFileDto? SubmitedFileDto { get; set; }
        public HttpStatusCode? StatusCode { get; set; } 
        public Exception? Exception { get; set; }   
    }
}
