using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services;
using Cooking_School_ASP.NET_.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages;

namespace Cooking_School_ASP.NET.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseService _courseServer;
        public CourseController(ILogger<CourseController> logger, ICourseService courseServer)
        {
            _logger = logger;
            _courseServer = courseServer;
        }



        [HttpPost("~/api/trainees/{traineeId}/courses/{courseId}/favorite")]
        public async Task<IActionResult> FavoriteCourse(int courseId, int traineeId)
        {
            var result = await _courseServer.FavoriteCourse(courseId, traineeId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.CourseDTO);
        }


        [HttpDelete("~/api/trainees/{traineeId}/courses/{courseId}/favorite")]
        public async Task<IActionResult> UnFavoriteCourse(int courseId, int traineeId)
        {
            var result = await _courseServer.UnFavoriteCourse(courseId, traineeId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.CourseDTO);
        }
    }
}
