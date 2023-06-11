using Backend_Controller_Burhan.Models;
using Cooking_School.Dtos.ApplicationDto;
using Cooking_School.Dtos.ChefDto;
using Cooking_School.Dtos.CourseDto;

namespace Cooking_School.Dtos.CookClassDto
{
    public class CookClassDTO : CreateCookClassDto
    {
        public int Id { get; set; }
        public ICollection<ProjectDTO>? Projects { get; set; }
        public ICollection<ApplicationDTO>? Applications { get; set; }
        public ChefDTO Chef { get; set; }
        public CourseDTO Course { get; set; }

    }
}
