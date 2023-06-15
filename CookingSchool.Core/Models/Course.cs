namespace Cooking_School.Core.Models
{
    public class Course : Audit
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int FavoriteN { get; set; } = 0;
        public decimal Price { get; set; }
        public Levels CourseLevel { get; set; }
        public ICollection<Trainee_Course> TraineeCourse { get; set; }
        public ICollection<Favorite_Course> FavoriteCourse { get; set; }
        public ICollection<CookClass> CookClasses { get; set; }

    }
}
