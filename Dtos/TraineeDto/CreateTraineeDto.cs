using Cooking_School.Dtos.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Dtos.TraineeDto
{
    public class CreateTraineeDto : CreateUserDto
    {

        [Required]
        public string Level { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public int CardN { get; set; }
        public IFormFile? image { get; set; }

    }
}
