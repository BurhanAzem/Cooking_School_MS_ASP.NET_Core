using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET_.Models;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public CookClass CookClass { get; set; }
        public ICollection<ProjectFile> projectFiles { get; set; }

    }
}
