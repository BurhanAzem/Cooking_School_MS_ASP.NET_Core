using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET_.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
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
