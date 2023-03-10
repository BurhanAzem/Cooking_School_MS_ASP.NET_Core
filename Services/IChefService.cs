using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services
{
    public interface IChefService
    {
        Task<ResponsDto<ChefDTO>> RegisterUser(CreateChefDto createTraineeDto);
        Task<ResponsDto<ChefDTO>> DeleteUser(int chefId);
        Task<ResponsDto<ChefDTO>> AddMealToFovarite(int idMeal, User CurrentUser);
        Task<ResponsDto<ChefDTO>> DeleteMealFromFovarite(int idMeal, User CurrentUser);
        Task<ResponsDto<ChefDTO>> LogOut(string token);
        Task<ResponsDto<ChefDTO>> UpdateUser(int id, UpdateChefDto updateTraineeDto);
        Task<ResponsDto<ChefDTO>> GetUserById(int id);
        Task<ResponsDto<ChefDTO>> GetAllUser(RequestParam requestParams = null);
        Task<ResponsDto<ChefDTO>> FavoriteChef(int traineeId, int chefId);
        Task<ResponsDto<ChefDTO>> UnFavoriteChef(int traineeId, int chefId);
        Task<ResponsDto<FavoriteChefDto>> GetAllFavoriteChefs(RequestParam requestParams = null);
    }
}
