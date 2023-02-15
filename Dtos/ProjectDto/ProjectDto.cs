using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET_.Models;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class ProjectDTO : CreateProjectDto
    {
        public int Id { get; set; }
        public CookClassDTO CookClass { get; set; }
        public ICollection<ProjectFileDTO> projectFiles { get; set; }

    }
}
