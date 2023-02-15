using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Models;

namespace Cooking_School_ASP.NET.Services
{
    public interface ITraineeService
    {
        Task<ResponsTraineeDto> RegisterUser(CreateTraineeDto createTraineeDto);
        Task<ResponsTraineeDto> AddMealToFovarite(int idMeal, User CurrentUser);
        Task<ResponsTraineeDto> DeleteMealFromFovarite(int idMeal);
        Task<ResponsTraineeDto> LogOut(HttpContext httpContext);
        Task<ResponsTraineeDto> UpdateUser(int id, UpdateTraineeDto updateTraineeDto);
        Task<ResponsTraineeDto> GetUserById(int id);
        Task<IList<TraineeDTO>> GetAllUsers(RequestParam requestParams = null);
        Task<ResponsTraineeDto> DeleteUser(int traineeId);



    }
}
