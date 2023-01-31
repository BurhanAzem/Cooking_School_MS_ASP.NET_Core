using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Models;

namespace Cooking_School_ASP.NET.Services
{
    public interface IAuthentication
    {
        Task<User> GetCurrentUser(HttpContext httpContext);
        Task<bool> IsAuthenticate(UserLoginDto userLoginDto);
        //Task<bool> IsAuthenticate(CreateUserDto createUserDto);
        Task<string> GenerateToken();
        Task<string> GenerateRefreshToken();
    }
}
