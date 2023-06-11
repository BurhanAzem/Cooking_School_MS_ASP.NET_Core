using Cooking_School.Dtos.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Dtos.TraineeDto
{
    public class UpdateTraineeDto : UpdateUserDto
    {
        public string? Level { get; set; }
        [DataType(DataType.CreditCard)]
        public int? CardN { get; set; }
        public IFormFile? image { get; set; }
    }
}
