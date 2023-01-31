using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Models
{
    public class FavoriteMeal_Trainee : Audit
    {
        [ForeignKey(nameof(Trainee))]
        public int TraineeId { get; set; }
        public  virtual Trainee Trainee { get; set; }
        public int MealId { get; set; }
    }
}
