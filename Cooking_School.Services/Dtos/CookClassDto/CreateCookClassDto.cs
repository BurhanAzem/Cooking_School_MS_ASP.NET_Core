using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Services.Dtos.CookClassDto
{
    public class CreateCookClassDto
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public TimeOnly StartingAt { get; set; }
        [Required]
        public TimeOnly EndingAt { get; set; }
        [Required]
        public List<string> ClassDays { get; set; }

    }
}
