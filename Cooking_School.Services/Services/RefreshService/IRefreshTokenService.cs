using Cooking_School.Core.Models;
namespace Cooking_School.Services.RefreshService
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GenerateRefreshToken();
        Task<RefreshToken> GetRefrshToken(int userId);
        Task SetRefreshToken(RefreshToken refreshToken);
    }
}
