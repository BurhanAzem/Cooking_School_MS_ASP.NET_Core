﻿using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Core.Models
{
    public class Favorite_Course : Audit
    {

        [ForeignKey(nameof(Trainee))]
        public int TraineeId { get; set; }
        public Trainee Trainee { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
