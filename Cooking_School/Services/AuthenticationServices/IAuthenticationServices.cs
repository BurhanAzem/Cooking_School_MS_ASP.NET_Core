using Backend_Controller_Burhan.Models;
using Cooking_School.Core.Models;
using Cooking_School.Dtos.UserDto;

namespace Cooking_School.Services.AuthenticationServices
{
    public interface IAuthenticationServices
    {
        Task<User> GetCurrentUser(HttpContext httpContext);
        Task<bool> IsAuthenticate(UserLoginDto userLoginDto);
        Task<string> GenerateToken(int userId);
    }
}
