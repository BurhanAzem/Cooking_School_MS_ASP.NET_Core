using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Core.Models
{
    public class Favorite_Chef : Audit
    {

        [ForeignKey(nameof(Trainee))]
        public int TraineeId { get; set; }
        public Trainee Trainee { get; set; }

        [ForeignKey(nameof(Chef))]
        public int ChefId { get; set; }
        public Chef Chef { get; set;}
    }
}
