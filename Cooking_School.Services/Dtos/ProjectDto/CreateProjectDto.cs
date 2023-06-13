using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Services.Dtos.CookClassDto
{
    public class CreateProjectDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public List<IFormFile>? Files { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirDate { get; set; }
    }
}
