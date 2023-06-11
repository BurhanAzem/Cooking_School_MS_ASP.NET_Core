using Cooking_School.Core.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Dtos;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos;

namespace Cooking_School.Services.TraineeService
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
        Task<ResponsDto<CookClassDTO>> GetAllCookClassesForTrainee(int traineeId, RequestParam requestParam = null);
        Task<ResponsDto<TraineeDTO>> DeleteUser(int traineeId);



    }
}
