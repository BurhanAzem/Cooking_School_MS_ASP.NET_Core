using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET_.Models;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Models
{ 
    public class Trainee : User
    {
        public Levels Level { get; set; }
        public string? ImagePath { get; set; }
        public int CardN { get; set; }
    }
}
