using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Controllers.AdminControllers
{
    [Route("api/admin/chefs")]
    [ApiController]
    public class AdminChefController : ControllerBase
    {
        private readonly ILogger<AdminCookClassController> _logger;

        public AdminChefController(ILogger<AdminCookClassController> logger)
        {
            _logger = logger;
        }
    }
}
