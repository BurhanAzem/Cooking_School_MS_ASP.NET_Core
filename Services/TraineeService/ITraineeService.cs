using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ApplicationDto;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services.TraineeService
{
    public interface ITraineeService
    {
        Task<ResponsDto<TraineeDTO>> RegisterUser(CreateTraineeDto createTraineeDto);
        Task<ResponsDto<TraineeDTO>> AddMealToFovarite(int idMeal, User CurrentUser);
        Task<ResponsDto<TraineeDTO>> DeleteMealFromFovarite(int idMeal);
        Task<ResponsDto<TraineeDTO>> LogOut(string token);
        Task<ResponsDto<TraineeDTO>> UpdateUser(int id, UpdateTraineeDto updateTraineeDto);
        Task<ResponsDto<TraineeDTO>> GetUserById(int id);
        Task<ResponsDto<TraineeDTO>> GetAllUsers(RequestParam requestParams = null);
        Task<ResponsDto<TraineeDTO>> DeleteUser(int traineeId);



    }
}
