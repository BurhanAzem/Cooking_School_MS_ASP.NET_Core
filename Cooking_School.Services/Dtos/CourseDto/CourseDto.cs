using Cooking_School.Core.Models;
using Cooking_School.Services.Dtos.CookClassDto;

namespace Cooking_School.Services.Dtos.CourseDto
{
    public class CourseDTO : CreateCourseDto
    {
        public int Id { get; set; }
        public int FavoriteN { get; set; }  
        public ICollection<Trainee> Trainees { get; set; }
        public ICollection<Trainee> FavoriteTrainees { get; set; }
        public ICollection<CookClass> CookClasses { get; set; }
    }
}
