using Microsoft.AspNetCore.Mvc;

namespace VodPlatform.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
            => Ok("VOD Platform API is running.");
    }
}