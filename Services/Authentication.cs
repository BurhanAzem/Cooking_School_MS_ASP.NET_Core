using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Hash;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.ModelUsed;
using System.Security.Cryptography;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Services
{
    public class Authentication : IAuthentication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IHashPassword _hash;
        private User _user;
        public Authentication(IUnitOfWork unitOfWork, IConfiguration configuration, IHashPassword hash)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration; 
            _hash = hash;   
        }
        public async Task<bool> IsAuthenticate(UserLoginDto userLoginDto)
        {
            _user = await _unitOfWork.Users.Get(q => userLoginDto.Id == q.Id);
            if (_user != null && _hash.verifyPassword(userLoginDto.Password, _user.PasswordHashed, _user.PasswordSlot))
            {
                return true;
            }
            _user = null;
            return false;
        }
        public async Task<string> GenerateToken()
        {
            var Claims = await GetClaims();
            var signingCredentials = GetSigningCredentials();
            var token = GenerateTokenOption(Claims, signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateTokenOption(List<Claim> claims, SigningCredentials signingCredentials)
        {
            var jwtSettings = _configuration.GetSection("jwt");
            var exipartion = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("lifetime").Value));
            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                expires: exipartion,
                claims: claims,
                signingCredentials: signingCredentials);
            return token;

        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.FullName),
                    new Claim(ClaimTypes.Role, Convert.ToString(_user.Discriminator)),
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.StreetAddress, _user.Address),
                };
                return claims;
 
        }

        public SigningCredentials GetSigningCredentials()
        {
            var hashed = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            return new SigningCredentials(hashed, SecurityAlgorithms.HmacSha256);
        }
        public async Task<RefreshToken> GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expire = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
        }


        public async Task<User> GetCurrentUser(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
                var CurrentUser = await _unitOfWork.Users.Get(x => x.Email == Email);
                return CurrentUser;
            }
            return null;
        }

        public async Task<string> LogOut(HttpContext httpContext)
        {
            var current = GetCurrentUser(httpContext);
            var user = await _unitOfWork.Users.Get(x => x.Id == current.Id);
            user.Token = null;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();
            return "Done";            
        }
    }
}
