using System.Net;

namespace Cooking_School_ASP.NET.Dtos.TraineeDto
{
    public class ResponsTraineeDto
    {
        public TraineeDTO? TraineeDto { get; set; } 
        public HttpStatusCode? StatusCode { get; set; }
        public Exception? Exception { get; set; }
    }
}
