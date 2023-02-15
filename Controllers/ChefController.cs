using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooking_School_ASP.NET.ModelUsed;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Web.WebPages;

namespace Cooking_School_ASP.NET_.Controllers
{
    [Route("api/chefs")]
    [ApiController]
    public class ChefController : ControllerBase
    {
        private readonly ILogger<ChefController> _logger;
        private readonly IAuthentication _authentication;
        private readonly IChefService _chefService;
        private readonly IMapper _mapper;
        public ChefController(ILogger<ChefController> logger, IAuthentication authentication, IChefService chefService, IMapper mapper)
        {
            _logger = logger;
            _authentication = authentication;
            _chefService = chefService;
            _mapper = mapper;
        }



        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginDto chefDto)
        {
            _logger.LogInformation($"Login Attempt for {nameof(chefDto)} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (await _authentication.IsAuthenticate(chefDto))
            {
                string token = await _authentication.GenerateToken();
                var refreshToken = await _authentication.GenerateRefreshToken();
                var user = await _chefService.GetUserById(chefDto.Id);
                var chef = _mapper.Map<Chef>(user.ChefDto);
                SetRefreshToken(refreshToken, chef);
                return Accepted(new TokenRequest { Token = token, RefreshToken = refreshToken.Token });
            }
            return Problem("Not Authenticated");
        }



        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            var result = await _chefService.LogOut(HttpContext);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ChefDto);
        }

        [HttpPost("refresh-token")]  //Authorize(Roles = "Admin")
        [Authorize]

        public async Task<IActionResult> RefreshToken()
        {
            var user = await _authentication.GetCurrentUser(HttpContext);
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.ExpireRefreshToken < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            var token = _authentication.GenerateToken();
            var newRefreshToken = await _authentication.GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, (Chef)user);

            return Ok(token);
        }




        

        [HttpGet("current")]

        public async Task<IActionResult> CurrentChef()
        {
            return Ok();
        }


        [HttpPost("meals/{mealId}/fovarite")]
        public async Task<IActionResult> AddMealToFovariteChef(int mealId)
        {
            var currentUser = await _authentication.GetCurrentUser(HttpContext);
            var result = await _chefService.AddMealToFovarite(mealId, currentUser);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ChefDto);
        }

        [HttpDelete("meals/{mealId}/fovarite")]
        public async Task<IActionResult> DeleteMealFromFovariteChef(int mealId)
        {
            var currentUser = await _authentication.GetCurrentUser(HttpContext);
            var result = await _chefService.DeleteMealFromFovarite(mealId, currentUser);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ChefDto);



        }

        [HttpPost("~/api/trainees/{traineeId}/chefs/{chefId}/favorite")]

        public async Task<IActionResult> FavoriteChef(int traineeId, int chefId)
        {
            var result = await _chefService.FavoriteChef(chefId, traineeId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ChefDto);
        }


        [HttpDelete("~/api/trainees/{traineeId}/chefs/{chefId}/favorite")]

        public async Task<IActionResult> UnFavoriteChef(int traineeId, int chefId)
        {
            var result = await _chefService.UnFavoriteChef(chefId, traineeId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ChefDto);
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, Chef user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expire
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.CreatedRefreshToken = newRefreshToken.Created;
            user.ExpireRefreshToken = newRefreshToken.Expire;
            var chefDto = _mapper.Map<UpdateChefDto>(user);
            _chefService.UpdateUser(user.Id, chefDto);
        }
        
    }


    
}
