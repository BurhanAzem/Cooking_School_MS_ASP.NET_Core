using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Models
{
    public class FavoriteMeal_chef : Audit 
    {
        [ForeignKey(nameof(Chef))]
        public int ChefId { get; set; }
        public virtual Chef Chef { get; set; }
        public int MealId { get; set; }
    }
}
