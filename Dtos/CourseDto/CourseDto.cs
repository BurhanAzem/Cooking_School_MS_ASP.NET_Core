using Backend_Controller_Burhan.Models;

namespace Cooking_School_ASP.NET.Dtos.CourseDto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public int FavoriteN { get; set; }  
        public ICollection<Trainee> Trainees { get; set; }
        public ICollection<Trainee> FavoriteTrainees { get; set; }
        public ICollection<CookClass> CookClasses { get; set; }
    }
}
