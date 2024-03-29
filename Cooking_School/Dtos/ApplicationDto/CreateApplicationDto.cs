﻿using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Dtos.ApplicationDto
{
    public class CreateApplicationDto
    {
        [Required]
        public int TraineeId { get; set; }
        [Required]
        public int CookClassId { get; set; }
    }
}
