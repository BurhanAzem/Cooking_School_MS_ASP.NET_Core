
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Core.Models
{ 
    public class Trainee : User
    {
        public Levels Level { get; set; }
        public string? ImagePath { get; set; }
        public int CardN { get; set; }
    }
}
