﻿using Cooking_School_ASP.NET.Dtos.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.TraineeDto
{
    public class UpdateTraineeDto : UpdateUserDto
    {
        public string? Level { get; set; }
        [DataType(DataType.CreditCard)]
        public int? CardN { get; set; }
        public IFormFile? image { get; set; }
    }
}
