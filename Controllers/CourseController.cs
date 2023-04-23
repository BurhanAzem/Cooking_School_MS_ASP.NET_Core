﻿using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services.AuthenticationServices;
using Cooking_School_ASP.NET.Services.CourseService;
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
        private readonly IAuthenticationServices _authenticationServices;
        public CourseController(ILogger<CourseController> logger, ICourseService courseServer, IAuthenticationServices authenticationServices)
        {
            _logger = logger;
            _courseServer = courseServer;
            _authenticationServices = authenticationServices;
        }



        [HttpPost("~/api/courses/{courseId}/favorite")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> FavoriteCourse(int courseId)
        {
            var trainee = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _courseServer.FavoriteCourse(courseId, trainee.Id);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }


        [HttpDelete("~/api/courses/{courseId}/favorite")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> UnFavoriteCourse(int courseId)
        {
            var trainee = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _courseServer.UnFavoriteCourse(courseId, trainee.Id);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }
    }
}