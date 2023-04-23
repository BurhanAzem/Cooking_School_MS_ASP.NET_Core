using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.ApplicationDto;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.ClassDaysDto;
using Cooking_School_ASP.NET.Dtos.CourseDto;
using Cooking_School_ASP.NET.Models;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
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
