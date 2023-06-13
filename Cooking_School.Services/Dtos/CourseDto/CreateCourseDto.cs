using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Services.Dtos.CookClassDto
{
    public class CreateCourseDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]
        public string CourseName { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Description Is Too Long")]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string CourseLevel { get; set; }  
    }
}
