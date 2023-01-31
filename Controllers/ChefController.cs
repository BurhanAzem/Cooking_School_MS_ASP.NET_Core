using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET_.Controllers
{
    [Route("api/chefs")]
    [ApiController]
    public class ChefController : ControllerBase
    {
        private readonly ILogger<ChefController> _logger;
        private readonly IAuthentication _authentication;
        private readonly IChefService _chefService;
        public ChefController(ILogger<ChefController> logger, IAuthentication authentication, IChefService chefService)
        {
            _logger = logger;
            _authentication = authentication;   
            _chefService= chefService;
        }
    


        [HttpGet("login")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginDto chefDto)
        {
            _logger.LogInformation($"Login Attempt for {nameof(chefDto)} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            if (await _authentication.IsAuthenticate(chefDto))
            {
                var token = await _authentication.GenerateToken();
                return Ok(token);
            }
            return Problem("Not Authenticated");
        }



        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            var result = await _chefService.LogOut(HttpContext);
            return Ok(result);
        }


        [HttpPost("signup")]
        public async Task<IActionResult> RegisterChef([FromBody] CreateChefDto createChefDto)
        {
            _logger.LogInformation($" Attempt Sinup for {createChefDto} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(createChefDto)}");
                return BadRequest(ModelState);
            }
            var result = _chefService.RegisterUser(createChefDto);
            return Ok(result);
        }


        [HttpPut("IdChef")]
        public async Task<IActionResult> UpdateChef(int IdChef, [FromBody] UpdateChefDto updateChefDto)
        {
            _logger.LogInformation($" Attempt Update for {updateChefDto} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(updateChefDto)}");
                return BadRequest(ModelState);
            }
            var result = _chefService.UpdateUser(IdChef, updateChefDto);
            return Ok(result);
        }

        [HttpGet("current")]
        public async Task<IActionResult> CurrentChef()
        {
            return Ok();
        }


        [HttpPost("meals/{mealId}/fovarite")]
        [Authorize]
        public async Task<IActionResult> AddMealToFovariteChef(int mealId)
        {
            var currentUser = await _authentication.GetCurrentUser(HttpContext);
            var result = await _chefService.AddMealToFovarite(mealId, currentUser);
            return Ok(result);
        }

        [HttpDelete("meals/{mealId}/fovarite")]
        [Authorize]
        public async Task<IActionResult> DeleteMealFromFovariteChef(int mealId)
        {
            var currentUser = await _authentication.GetCurrentUser(HttpContext);
            var result = await _chefService.DeleteMealFromFovarite(mealId, currentUser);
            return Ok(result);
        }





        [HttpPost("~/api/trainees/{traineesId}/chefs/{chefId}/favorite")]
        public async Task<IActionResult> FavoriteChef()
        {
                return Ok();
            }


        [HttpDelete("~/api/trainees/{traineesId}/chefs/{chefId}/favorite")]
        public async Task<IActionResult> UnFavoriteChef()
        {
                return Ok();
            }


    }
}
