using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Cooking_School_ASP.NET.Services.TraineeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.WebPages;
using System.Net.Http;
using Cooking_School_ASP.NET.Dtos.AdminDto;
using Cooking_School_ASP.NET.Services.AuthenticationServices;
using Cooking_School_ASP.NET.Services.RefreshService;

namespace Cooking_School_ASP.NET_.Controllers
{
    [Route("api/trainees")]
    [ApiController]

    public class TraineeController : ControllerBase
    {
        private readonly ILogger<TraineeController> _logger;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ITraineeService _traineeService;
        private readonly IMapper _mapper;
        public TraineeController(ILogger<TraineeController> logger, IAuthenticationServices authentication, ITraineeService traineeService, IMapper mapper, IRefreshTokenService refreshTokenService)
        {
            _logger = logger;
            _authenticationServices = authentication;
            _traineeService = traineeService;
            _mapper = mapper;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginDto traineeDto)
        {

            _logger.LogInformation($"Login Attempt for {nameof(traineeDto)} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _authenticationServices.IsAuthenticate(traineeDto))
            {
                string token = await _authenticationServices.GenerateToken(traineeDto.Id);
                var refreshToken = await _refreshTokenService.GenerateRefreshToken();
                refreshToken.UserId = traineeDto.Id;
                await SetRefreshToken(refreshToken);
                return Accepted(new TokenRequest { Token = token, RefreshToken = refreshToken.Token });
            }
            return BadRequest("User not authenticate");
        }

        [HttpPost("logout")]
        [Authorize(Roles = "Administrator, Trainee")]
        public async Task<IActionResult> LogOut()
        {
            _logger.LogInformation($"Attempt to logout ");
            string token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            { 
                var result = await _traineeService.LogOut(token);
                if (result.Exception is not null)
                {
                    throw new StatusCodeException(result.StatusCode.Value, result.Exception);
                }
                return Ok("Done");
            }
            _logger.LogInformation($"fail Attempt to logout ");
            throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, new Exception("there is problem in the token"));
        }


        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] CreateTraineeDto traineeDto)
        {
            if (traineeDto.image == null || traineeDto.image.Length == 0)
                return Content("file not selected");

            _logger.LogInformation($" Attempt Sinup for {traineeDto} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(traineeDto)}");
                return BadRequest(ModelState);
            }
            var result = await _traineeService.RegisterUser(traineeDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }

        [HttpPost("refresh-token"), Authorize(Roles = "Admin")]
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

        [HttpPut("{traineeId}")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> UpdateTrainee(int traineeId, [FromForm] UpdateTraineeDto updateTraineeDto)
        {
            _logger.LogInformation($" Attempt Update for {updateTraineeDto} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(updateTraineeDto)}");
                return BadRequest(ModelState);
            }
            var result = await _traineeService.UpdateUser(traineeId, updateTraineeDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }


        [HttpPost("current")]
        [Authorize(Roles = "Trainee, Administrator, Chef")]
        public async Task<IActionResult> LogIn()
        {
            var result = await _authenticationServices.GetCurrentUser(HttpContext);
            return Ok(result);
        }





        [HttpPost("meals/{mealId}/fovarite")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> AddMealToFovariteTrainee(int mealId)
        {
            var currentUser = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _traineeService.AddMealToFovarite(mealId, currentUser);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }



        [HttpDelete("meals/{mealId}/fovarite")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> DeleteMealFromFovariteTrainee(int mealId)
        {
            var result = await _traineeService.DeleteMealFromFovarite(mealId);
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
