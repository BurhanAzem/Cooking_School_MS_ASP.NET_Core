using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services.AuthenticationServices
{
    public interface IAuthenticationServices
    {
        Task<User> GetCurrentUser(HttpContext httpContext);
        Task<bool> IsAuthenticate(UserLoginDto userLoginDto);
        Task<string> GenerateToken(int userId);
    }
}
