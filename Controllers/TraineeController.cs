using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.WebPages;
using System.Net.Http;

namespace Cooking_School_ASP.NET_.Controllers
{
    [Route("api/trainees")]
    [ApiController]

    public class TraineeController : ControllerBase
    {
        private readonly ILogger<TraineeController> _logger;
        private readonly IAuthentication _authentication;
        private readonly ITraineeService _traineeService;
        private readonly IMapper _mapper;
        public TraineeController(ILogger<TraineeController> logger, IAuthentication authentication, ITraineeService traineeService, IMapper mapper)
        {
            _logger = logger;
            _authentication = authentication;
            _traineeService = traineeService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginDto traineeDto)
        {

            _logger.LogInformation($"Login Attempt for {nameof(traineeDto)} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (await _authentication.IsAuthenticate(traineeDto))
            {
                string token = await _authentication.GenerateToken();
                var refreshToken = await _authentication.GenerateRefreshToken();
                var user = await _traineeService.GetUserById(traineeDto.Id);
                var trainee = _mapper.Map<Trainee>(user.TraineeDto);
                SetRefreshToken(refreshToken, trainee);
                return Accepted(new TokenRequest { Token = token, RefreshToken = refreshToken.Token });
            }
            return null;

        }
        

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            var result = await _traineeService.LogOut(HttpContext);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.TraineeDto);
        }


        [HttpPost()]
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
            return Ok(result.TraineeDto);
        }

        [HttpPost("refresh-token"), Authorize(Roles = "Admin")]
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
            SetRefreshToken(newRefreshToken, (Trainee)user);

            return Ok(token);
        }

        [HttpPut("{traineeId}")]
        public async Task<IActionResult> UpdateTrainee(int traineeId, [FromBody] UpdateTraineeDto updateTraineeDto)
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
            return Ok(result.TraineeDto);
        }


        [HttpPost("current")]
        public async Task<IActionResult> LogIn()
        {
            var result = await _authentication.GetCurrentUser(HttpContext);
            return Ok(result);
        }





        [HttpPost("meals/{mealId}/fovarite")]
        [Authorize]
        public async Task<IActionResult> AddMealToFovariteTrainee(int mealId)
        {
            var currentUser = await _authentication.GetCurrentUser(HttpContext);
            var result = await _traineeService.AddMealToFovarite(mealId, currentUser);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.TraineeDto);
        }



        [HttpDelete("meals/{mealId}/fovarite")]
        [Authorize]
        public async Task<IActionResult> DeleteMealFromFovariteTrainee(int mealId)
        {
            var result = await _traineeService.DeleteMealFromFovarite(mealId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.TraineeDto);
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, Trainee user)
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
            var traineeDto = _mapper.Map<UpdateTraineeDto>(user);
            _traineeService.UpdateUser(user.Id, traineeDto);
        }
    }




}
