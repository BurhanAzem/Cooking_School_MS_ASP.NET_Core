﻿using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Models
{
    public class Trainee_Course : Audit
    {

        [ForeignKey(nameof(Trainee))]
        public int TraineeId { get; set; }  
        public Trainee Trainee { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set;}
        public Total_Mark? Total_Mark { get; set; }

    }
}
