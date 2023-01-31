using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Models;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class CookClassDto
    {
        public int Id { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<ApplicationT> Applications { get; set; }
        public Chef Chef { get; set; }
        public Course Course { get; set; }

    }
}
