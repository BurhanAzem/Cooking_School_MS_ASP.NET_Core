using AutoMapper;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Cooking_School_ASP.NET_.Controllers
{
    [Route("api/trainees")]
    [ApiController]

    public class TraineeController : ControllerBase
    {
        private readonly ILogger<TraineeController> _logger;
        private readonly IAuthentication _authentication;
        private readonly ITraineeService _traineeService;

        public TraineeController(ILogger<TraineeController> logger, IAuthentication authentication, ITraineeService traineeService)
        {
            _logger = logger;
            _authentication = authentication;
            _traineeService = traineeService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginDto traineeDto)
        { 

            _logger.LogInformation($"Login Attempt for {nameof(traineeDto)} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (await _authentication.IsAuthenticate(traineeDto))
                {
                    var token = await _authentication.GenerateToken();
                    return Ok(token);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the ");
                return Problem($"Something Went Wrong in the ", statusCode: 500);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            var result = await _traineeService.LogOut(HttpContext);
            return Ok(result);
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] CreateTraineeDto traineeDto)
        {

            _logger.LogInformation($" Attempt Sinup for {traineeDto} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(traineeDto)}");
                return BadRequest(ModelState);
            }
            var result = _traineeService.RegisterUser(traineeDto);
            return Ok(result);
        }


        [HttpPut("IdTrainee")]
        public async Task<IActionResult> UpdateTrainee(int IdTrainee, [FromBody] UpdateTraineeDto updateTraineeDto)
        {
            _logger.LogInformation($" Attempt Update for {updateTraineeDto} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(updateTraineeDto)}");
                return BadRequest(ModelState);
            }
            var result = _traineeService.UpdateUser(IdTrainee, updateTraineeDto);
            return Ok(result);
        }


        [HttpPost("current")]
        public async Task<IActionResult> LogIn()
        {
            return Ok();
        }





        [HttpPost("meals/{mealId}/fovarite")]
        [Authorize]
        public async Task<IActionResult> AddMealToFovariteTrainee(int mealId)
        {
            var currentUser = await _authentication.GetCurrentUser(HttpContext);
            var result = await _traineeService.AddMealToFovarite(mealId, currentUser);
            return Ok(result);
        }



        [HttpDelete("meals/{mealId}/fovarite")]
        [Authorize]
        public async Task<IActionResult> DeleteMealFromFovariteTrainee(int mealId)
        {
            var result = await _traineeService.DeleteMealFromFovarite(mealId);
            return Ok(result);
        }


    }




}
