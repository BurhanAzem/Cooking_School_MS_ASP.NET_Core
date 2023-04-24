using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class UpdateSubmitedFileDto
    {
        public int? TraineeId { get; set; }
        public int? ProjectId { get; set; }
        [DataType(DataType.Upload)]
        public List<IFormFile>? Files { get; set; }
    }
}
