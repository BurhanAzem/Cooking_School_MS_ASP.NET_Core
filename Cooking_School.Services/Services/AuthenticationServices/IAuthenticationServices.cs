using Cooking_School.Core.Models;
using Cooking_School.Services.Dtos.UserDto;
using Microsoft.AspNetCore.Http;

namespace Cooking_School.Services.AuthenticationServices
{
    public interface IAuthenticationServices
    {
        Task<User> GetCurrentUser(HttpContext httpContext);
        Task<bool> IsAuthenticate(UserLoginDto userLoginDto);
        Task<string> GenerateToken(int userId);
    }
}
