using Backend_Controller_Burhan.Models;
using Cooking_School.Core.Models;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School.Dtos.SubmitedFileDto;
using Cooking_School.Dtos.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Dtos.TraineeDto
{
    public class TraineeDTO : UserDTO
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public int CardN { get; set; }
        public string ImagePath { get; set; }   
        public ICollection<Trainee_Course>? TraineeCourse { get; set; }
        public ICollection<SubmitedFileDTO>? SubmitedFile { get; set; }
        public  ICollection<FavoriteMeal_Trainee>? FavoriteMealTrainee { get; set; }
        public ICollection<Favorite_Course>? FavoriteCourse { get; set; }
        public ICollection<Favorite_Chef>? FavoriteChef { get; set; }
    }
}
