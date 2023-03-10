using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class UpdateProjectDto 
    {
        public int? CookClassId { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ExpirDate { get; set; }
    }
}
