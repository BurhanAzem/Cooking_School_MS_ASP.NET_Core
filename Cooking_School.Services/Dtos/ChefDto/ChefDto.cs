using Cooking_School.Core.Models;
using Cooking_School.Services.Dtos.UserDto;

namespace Cooking_School.Services.Dtos.ChefDto
{
    public class ChefDTO : UserDTO
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public string CvPath { get; set; }    
        public ICollection<CookClass>? CookClasses { get; set; }
        public ICollection<Favorite_Chef>? FavoriteChef { get; set; }
        public ICollection<FavoriteMeal_chef>? FavoriteMealchef { get; set; }
    }
}
