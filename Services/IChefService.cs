using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Models;

namespace Cooking_School_ASP.NET.Services
{
    public interface IChefService
    {
        Task<ResponsChefDto> RegisterUser(CreateChefDto createTraineeDto);
        Task<ResponsChefDto> AddMealToFovarite(int idMeal, User CurrentUser);
        Task<ResponsChefDto> DeleteMealFromFovarite(int idMeal, User CurrentUser);
        Task<ResponsChefDto> LogOut(HttpContext httpContext);
        Task<ResponsChefDto> UpdateUser(int id, UpdateChefDto updateTraineeDto);
        Task<ResponsChefDto> GetUserById(int id);
        Task<IList<ChefDto>> GetAllUser();
    }
}
