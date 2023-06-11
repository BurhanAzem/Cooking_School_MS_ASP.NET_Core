namespace Cooking_School.Core.Models
{
    public class Course : Audit
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int FavoriteN { get; set; } = 0;
        public decimal Price { get; set; }
        public Levels CourseLevel { get; set; }
        public virtual ICollection<Trainee_Course> TraineeCourse { get; set; }
        public virtual ICollection<Favorite_Course> FavoriteCourse { get; set; }
        public virtual ICollection<CookClass> CookClasses { get; set; }

    }
}
