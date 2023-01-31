using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class CreateCookClassDto
    {

        public int CourseId { get; set; }

        public int ChefId { get; set; }
        [Required]
        public DateTime StartingAt { get; set; }
        [Required]
        public DateTime EndingAt { get; set; }
        [Required]
        public DayOfWeek[] Days { get; set; }

    }
}
