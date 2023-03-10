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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IHashPassword _hash;
        public AuthenticationServices(IUnitOfWork unitOfWork, IConfiguration configuration, IHashPassword hash)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration; 
            _hash = hash;   
        }
        public async Task<bool> IsAuthenticate(UserLoginDto userLoginDto)
        {
            var user = await _unitOfWork.Users.Get(q => userLoginDto.Id == q.Id);

            if (user != null && _hash.verifyPassword(userLoginDto.Password, user.PasswordHashed, user.PasswordSlot))
            {
                return true;
            }
            user = null;
            return false;
        }
        public async Task<string> GenerateToken(int userId)
        {
            var user = await _unitOfWork.Users.Get(q => userId == q.Id);
            var Claims = await GetClaims(user);
            var signingCredentials = GetSigningCredentials();
            var token = GenerateTokenOption(Claims, signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateTokenOption(List<Claim> claims, SigningCredentials signingCredentials)
        {
            var jwtSettings = _configuration.GetSection("jwt");
            var exipartion = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("lifetime").Value));
            var token = new JwtSecurityToken(
                audience: jwtSettings.GetSection("Audience").Value,
                issuer: jwtSettings.GetSection("Issuer").Value,
                expires: exipartion,
                claims: claims,
                signingCredentials: signingCredentials);
            return token;

        }

        private async Task<List<Claim>> GetClaims(User _user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.FullName),
                    new Claim(ClaimTypes.Role, _user.Discriminator),
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.StreetAddress, _user.Address),
                };
                return claims;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var hashed = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            return new SigningCredentials(hashed, SecurityAlgorithms.HmacSha256);
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
    }
}
