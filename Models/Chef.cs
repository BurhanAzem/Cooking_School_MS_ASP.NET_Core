using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Models
{
    public class Chef : User
    {

        public string CvPath { get; set; }
        public decimal Salary { get; set; }
        public int FavoriteN { get; set; } = 0;
        public virtual ICollection<CookClass> CookClasses { get; set; }
        public virtual ICollection<Favorite_Chef> FavoriteChef { get; set; }
        public virtual ICollection<FavoriteMeal_chef> FavoriteMealchef { get; set; }
    }
}
