using Cooking_School.Services.Dtos.UserDto;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Services.Dtos.TraineeDto
{
    public class UpdateTraineeDto : UpdateUserDto
    {
        public string? Level { get; set; }
        [DataType(DataType.CreditCard)]
        public int? CardN { get; set; }
        public IFormFile? image { get; set; }
    }
}
