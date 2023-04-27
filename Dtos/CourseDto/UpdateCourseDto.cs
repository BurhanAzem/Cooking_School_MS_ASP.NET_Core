using Cooking_School_ASP.NET.Dtos.CourseDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class UpdateCourseDto
    {
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]
        public string ?CourseName { get; set; }
        [StringLength(maximumLength: 250, ErrorMessage = "Description Is Too Long")]
        public string ?Description { get; set; }
        public decimal ?Price { get; set; }
        public string? CourseLevel { get; set; }
    }
}
