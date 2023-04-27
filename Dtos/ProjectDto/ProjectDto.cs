using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.ProjectFileDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET_.Models;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public int CookClassId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime ExpirDate { get; set; }

        //public CookClassDTO CookClass { get; set; }
        public ICollection<ProjectFileDTO> projectFiles { get; set; }

    }
}
