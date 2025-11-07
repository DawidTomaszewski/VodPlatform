using Microsoft.AspNetCore.Mvc;
using VodPlatform.Core.Application.Abstractions.Services;

namespace VodPlatform.WebApi.Controllers
{
    [ApiController]
    [Route("api/dash")]
    public class DashStreamingController : ControllerBase
    {
        private readonly IDashProxyService _dashProxyService;

        public DashStreamingController(IDashProxyService dashProxyService)
        {
            _dashProxyService = dashProxyService;
        }

        [HttpGet("{*relativePath}")]
        public async Task<IActionResult> ProxyDashFile([FromRoute] string relativePath)
        {
            try
            {
                var stream = await _dashProxyService.GetMpdFileUrl(relativePath);

                return Ok(stream);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
