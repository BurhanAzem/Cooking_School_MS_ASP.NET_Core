using Cooking_School_ASP.NET.Dtos.ClassDaysDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class UpdateCookClassDto
    {
        public int? CourseId { get; set; }
        public int? ChefId { get; set; }
        public DateTime? StartingAt { get; set; }
        public DateTime? EndingAt { get; set; }
    }
}
