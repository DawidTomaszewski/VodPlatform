using MediatR;
using Microsoft.AspNetCore.Mvc;
using VodPlatform.Core.Application.Actions.Commands;
using VodPlatform.Core.Application.Actions.Queries;

namespace VodPlatform.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchlistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WatchlistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWatchlist(string userId)
            => Ok(await _mediator.Send(new GetWatchlistByIdQuery(userId)));

        [HttpGet("{userId}/items")]
        public async Task<IActionResult> GetWatchItems(string userId)
            => Ok(await _mediator.Send(new GetWatchItemsQuery(userId)));

        [HttpGet("{userId}/items/{watchItemId}")]
        public async Task<IActionResult> GetWatchItemById(int watchItemId, string userId)
            => Ok(await _mediator.Send(new GetWatchItemByIdQuery(watchItemId, userId)));

        [HttpPost("AddWatchlist/{userId}")]
        public async Task<IActionResult> AddWatchlist(string userId)
            => Ok(await _mediator.Send(new AddWatchlistCommand(userId)));

        [HttpDelete("RemoveWatchlist/{userId}")]
        public async Task<IActionResult> RemoveWatchlist(string userId)
            => Ok(await _mediator.Send(new RemoveWatchlistCommand(userId)));

        [HttpPost("{watchlistId}/add-movie")]
        public async Task<IActionResult> AddMovie([FromBody] AddWatchItemMovieCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("{watchlistId}/add-series")]
        public async Task<IActionResult> AddSeries([FromBody] AddWatchItemSeriesCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("{userId}/items/{watchItemId}")]
        public async Task<IActionResult> RemoveItem(int watchItemId, string userId)
            => Ok(await _mediator.Send(new RemoveItemWatchItemCommand(watchItemId, userId)));
    }
}
