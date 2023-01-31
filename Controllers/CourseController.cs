using Cooking_School_ASP.NET_.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        public CourseController(ILogger<CourseController> logger)
        {
            _logger= logger;
        }



        [HttpPost("~/api/trainees/{traineeId}/courses/{courseId}/favorite")]
        public async Task<IActionResult> FavoriteCourse()
        {
            return Ok();
        }

        [HttpDelete("~/api/trainees/{traineeId}/courses/{courseId}/favorite")]
        public async Task<IActionResult> UnFavoriteCourse()
        {
            return Ok();
        }
    }
}
