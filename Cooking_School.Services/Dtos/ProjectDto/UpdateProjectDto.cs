using Cooking_School.Services.Dtos.CookClassDto;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Services.Dtos.ProjectDto
{
    public class UpdateProjectDto : CreateProjectDto
    {
        public int? CookClassId { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        public List<IFormFile>? Files { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ExpirDate { get; set; }
    }
}
