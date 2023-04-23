using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class CreateSubmitedFileDto
    {
        [Required]
        public int TraineeId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile content { get; set; }
    }
}
