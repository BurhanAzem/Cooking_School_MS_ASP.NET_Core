using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;

namespace Cooking_School_ASP.NET.Dtos.ApplicationDto
{
    public class ApplicationDTO : CreateApplicationDto
    {
        public int Id { get; set; }
        public CookClassDTO CookClass { get; set; }
        public TraineeDTO Trainee { get; set; }

    }
}
