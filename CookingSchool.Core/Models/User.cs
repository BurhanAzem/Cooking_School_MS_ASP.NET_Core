using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Backend_Controller_Burhan.Models;

namespace Cooking_School.Core.Models
{
    public abstract class User : Audit
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
        public string Discriminator { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public byte[] PasswordHashed { get; set; }
        public byte[] PasswordSlot { get; set; }
        public int PhoneNumber { get; set; }
        public virtual IList<Trainee_Course> TraineeCourses { get; set; }
        public virtual ICollection<ProjectTrainee> ProjectTrainees { get; set; }
        public virtual ICollection<FavoriteMeal_Trainee> FavoriteMealTrainees { get; set; }
        public virtual ICollection<Favorite_Course> FavoriteCourses { get; set; }
        public virtual ICollection<Favorite_Chef> FavoriteChefs { get; set; }
        public virtual ICollection<CookClass> CookClasses { get; set; }
        public virtual ICollection<FavoriteMeal_chef> FavoriteMealchefs { get; set; }

    }
}
