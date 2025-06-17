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

        [HttpGet("ProxyDashFile/{*relativePath}")]
        public async Task<IActionResult> ProxyDashFile([FromRoute] string relativePath, CancellationToken cancellationToken)
        {
            try
            {
                var stream = await _dashProxyService.GetRemoteFileAsync(relativePath, cancellationToken);
                var contentType = _dashProxyService.GetContentType(relativePath);

                return File(stream, contentType, enableRangeProcessing: false);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
