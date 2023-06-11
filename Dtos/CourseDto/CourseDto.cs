using Cooking_School.Core.Models;
using Cooking_School.Dtos.CookClassDto;


namespace Cooking_School.Dtos.CourseDto
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
