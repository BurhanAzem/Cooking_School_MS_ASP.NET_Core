using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooking_School_ASP.NET.ModelUsed;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Web.WebPages;
using Cooking_School_ASP.NET.Dtos.AdminDto;
using Cooking_School_ASP.NET.Services.AuthenticationServices;
using Cooking_School_ASP.NET.Services.ChefService;
using Cooking_School_ASP.NET.Services.RefreshService;
using Cooking_School_ASP.NET.Dtos;

namespace Cooking_School_ASP.NET_.Controllers
{
    [Route("api/chefs")]
    [ApiController]
    public class ChefController : ControllerBase
    {
        private readonly ILogger<ChefController> _logger;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IChefService _chefService;
        private readonly IMapper _mapper;
        public ChefController(ILogger<ChefController> logger, IAuthenticationServices authentication, IChefService chefService, IMapper mapper, IRefreshTokenService refreshTokenService)
        {
            _logger = logger;
            _authenticationServices = authentication;
            _chefService = chefService;
            _mapper = mapper;
            _refreshTokenService = refreshTokenService;
        }



        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginDto chefDto)
        {
            _logger.LogInformation($"Login Attempt for {nameof(chefDto)} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _authenticationServices.IsAuthenticate(chefDto))
            {
                string token = await _authenticationServices.GenerateToken(chefDto.Id);
                var refreshToken = await _refreshTokenService.GenerateRefreshToken();
                refreshToken.UserId = chefDto.Id;
                await SetRefreshToken(refreshToken);
                return Accepted(new TokenRequest { Token = token, RefreshToken = refreshToken.Token });
            }
            return BadRequest("User not authenticate");
        }

        [HttpPost("logout")]
        [Authorize(Roles = "Administrator, Chef")]
        public async Task<IActionResult> LogOut()
        {
            _logger.LogInformation($"Attempt to logout ");
            string token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var result = await _chefService.LogOut(token);
                if (result.Exception is not null)
                {
                    throw new StatusCodeException(result.StatusCode.Value, result.Exception);
                }
                return Ok("Done");
            }
            _logger.LogInformation($"fail Attempt to logout ");
            throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, new Exception("there is problem in the token"));
        }

        [HttpPost("refresh-token")]  //Authorize(Roles = "Admin")
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await _authenticationServices.GetCurrentUser(HttpContext);
            var refreshToken = Request.Cookies["refreshToken"];
            RefreshToken userRefreshToken = await _refreshTokenService.GetRefrshToken(user.Id);
            if (!userRefreshToken.Token.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (userRefreshToken.ExpirationDate < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            var token = await _authenticationServices.GenerateToken(user.Id);
            var newRefreshToken = await _refreshTokenService.GenerateRefreshToken();
            newRefreshToken.UserId = user.Id;
            await SetRefreshToken(newRefreshToken);
            return Accepted(new TokenRequest { Token = token, RefreshToken = newRefreshToken.Token });
        }
        



        

        //[HttpGet("current")]
        //[Authorize(Roles = "Administrator, Chef")]
        //public async Task<IActionResult> CurrentChef()
        //{
        //    return Ok();
        //}


        [HttpPost("{chefId}/meals/{mealId}/fovarite")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> AddMealToFovariteChef(int mealId)
        {
            var currentUser = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _chefService.AddMealToFovarite(mealId, currentUser);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }

        [HttpDelete("{chefId}/meals/{mealId}/fovarite")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> DeleteMealFromFovariteChef(int mealId)
        {
            var currentUser = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _chefService.DeleteMealFromFovarite(mealId, currentUser);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);



        }

        [HttpPost("{chefId}/favorite")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> FavoriteChef(int chefId)
        {
            var user = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _chefService.FavoriteChef(chefId, user.Id);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }


        [HttpDelete("{chefId}/favorite")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> UnFavoriteChef(int traineeId, int chefId)
        {
            var user = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _chefService.UnFavoriteChef(chefId, user.Id);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }

        private async Task SetRefreshToken(RefreshToken newRefreshToken)
        {
            await _refreshTokenService.SetRefreshToken(newRefreshToken);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpirationDate
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }

    }
  
}
