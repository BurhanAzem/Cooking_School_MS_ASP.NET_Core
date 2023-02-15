using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Models;

namespace Cooking_School_ASP.NET.Dtos.ChefDto
{
    public class ChefDTO : CreateChefDto
    {
        public int Id { get; set; }

        public ICollection<CookClass>? CookClasses { get; set; }
        public ICollection<Favorite_Chef>? FavoriteChef { get; set; }
        public ICollection<FavoriteMeal_chef>? FavoriteMealchef { get; set; }
    }
}
