using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Dtos.CookClassDto
{
    public class UpdateCookClassDto
    {
        public int? CourseId { get; set; }
        public int? ChefId { get; set; }
        public TimeOnly? StartingAt { get; set; }
        public TimeOnly? EndingAt { get; set; }
        public List<string>? ClassDays { get; set; }
    }
}
