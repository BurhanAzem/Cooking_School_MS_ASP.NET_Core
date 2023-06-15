using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Core.Models
{
    public class FavoriteMeal_chef : Audit 
    {
        [ForeignKey(nameof(Chef))]
        public int ChefId { get; set; }
        public Chef Chef { get; set; }
        public int MealId { get; set; }
    }
}
