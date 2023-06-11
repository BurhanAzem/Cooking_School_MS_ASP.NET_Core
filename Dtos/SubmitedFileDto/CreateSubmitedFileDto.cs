using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Dtos.SubmitedFileDto
{
    public class CreateSubmitedFileDto
    {
        [Required]
        public int ?TraineeId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public List<IFormFile> Files { get; set; }
    }
}
