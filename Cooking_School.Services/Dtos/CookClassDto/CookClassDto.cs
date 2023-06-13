using Backend_Controller_Burhan.Models;
using Cooking_School.Services.Dtos.ApplicationDto;
using Cooking_School.Services.Dtos.ChefDto;
using Cooking_School.Services.Dtos.CourseDto;
using Cooking_School.Services.Dtos.ProjectDto;

namespace Cooking_School.Services.Dtos.CookClassDto
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
