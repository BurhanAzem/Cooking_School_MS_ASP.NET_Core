using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Core.Models
{
    public class Chef : User
    {

        public string CvPath { get; set; }
        public decimal Salary { get; set; }
        public int FavoriteN { get; set; } = 0;
        public ICollection<CookClass> CookClasses { get; set; }
        public ICollection<Favorite_Chef> FavoriteChef { get; set; }
        public ICollection<FavoriteMeal_chef> FavoriteMealchef { get; set; }
    }
}
