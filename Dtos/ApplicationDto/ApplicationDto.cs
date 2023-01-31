using Backend_Controller_Burhan.Models;

namespace Cooking_School_ASP.NET.Dtos.ApplicationDto
{
    public class ApplicationDto : CreateApplicationDto
    {
        public int Id { get; set; }
        public CookClass CookClass { get; set; }
        public Trainee Trainee { get; set; }

    }
}
