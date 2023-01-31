using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminCookClassController> _logger;

        public AdminController(ILogger<AdminCookClassController> logger)
        {
            _logger = logger;
        }
    }
}
