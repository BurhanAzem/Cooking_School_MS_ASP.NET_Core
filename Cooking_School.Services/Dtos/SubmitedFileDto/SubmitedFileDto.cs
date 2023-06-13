using Cooking_School.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Cooking_School.Services.Dtos.SubmitedFileDto
{
    public class SubmitedFileDTO
    {
        public int Id { get; set; }
        public DateTime SubmitedDate { get; set; }
        public int ProjectTraineeId { get; set; }
        public string FilePath { get; set; }
    }
}
