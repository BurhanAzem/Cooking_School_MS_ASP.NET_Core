using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class SubmitedFileDto
    {
        public int Id { get; set; }
        public Trainee Trainee { get; set; }
        public DateTime SubmitedDate { get; set; }
        public int TraineeId { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}
