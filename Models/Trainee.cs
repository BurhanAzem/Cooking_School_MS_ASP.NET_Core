using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET_.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend_Controller_Burhan.Models
{
    public class Trainee : User
    {
        public Level Level { get; set; }
        public byte? image { get; set; }
        public int CardN { get; set; }
    }
}
