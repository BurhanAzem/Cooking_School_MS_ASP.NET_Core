using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET_.Models;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.TraineeDto
{
    public class TraineeDTO : CreateTraineeDto
    {
        public int Id { get; set; }
        public ICollection<Trainee_Course>? TraineeCourse { get; set; }
        public ICollection<SubmitedFileDto>? ProjectFile { get; set; }
        public  ICollection<FavoriteMeal_Trainee>? FavoriteMealTrainee { get; set; }
        public ICollection<Favorite_Course>? FavoriteCourse { get; set; }
        public ICollection<Favorite_Chef>? FavoriteChef { get; set; }
    }
}
