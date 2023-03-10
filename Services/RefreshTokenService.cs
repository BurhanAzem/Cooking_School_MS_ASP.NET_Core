using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using System.Security.Cryptography;

namespace Cooking_School_ASP.NET.Services
{
    public class RefreshTokenServicse : IRefreshTokenService
    {
        private IUnitOfWork _unitOfWork;
        public RefreshTokenServicse(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RefreshToken> GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpirationDate = DateTime.Now.AddDays(7),
                IssuedDate = DateTime.Now
            };
        }

        public async Task<RefreshToken> GetRefrshToken(int userId)
        {
            var refreshToken = await _unitOfWork.RefreshTokens.Get(r => r.UserId == userId);
            return refreshToken;
        }

        public async Task SetRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken rToken = await _unitOfWork.RefreshTokens.Get(r => r.UserId == refreshToken.UserId);
            if (rToken is not null)
            {
                await _unitOfWork.RefreshTokens.Delete(rToken.Id);
                await _unitOfWork.Save();
            }
            await _unitOfWork.RefreshTokens.Insert(refreshToken);
            await _unitOfWork.Save();
        }
    }
}
