using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ClassDaysDto;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
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
