﻿using Cooking_School_ASP.NET.Models;

namespace Cooking_School_ASP.NET.Services
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GenerateRefreshToken();
        Task<RefreshToken> GetRefrshToken(int userId);
        Task SetRefreshToken(RefreshToken refreshToken);
    }
}
