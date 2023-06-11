using Backend_Controller_Burhan.Models;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos;


namespace Cooking_School.Dtos.ApplicationDto
{
    public class ApplicationDTO : CreateApplicationDto
    {
        public int Id { get; set; }
        public string status { get; set; }
        public DateTime DateOfAppaly { get; set; }
        public CookClassDTO? CookClass { get; set; }
        public TraineeDTO? Trainee { get; set; }

    }
}
