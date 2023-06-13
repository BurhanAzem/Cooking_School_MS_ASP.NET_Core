using Backend_Controller_Burhan.Models;
using Cooking_School.Services.Dtos.SubmitedFileDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Services.Dtos.ProjectDto
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public int CookClassId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime ExpirDate { get; set; }

        //public CookClassDTO CookClass { get; set; }
        public ICollection<SubmitedFileDTO> projectFiles { get; set; }
        
    }
}
