using MediatR;
using Microsoft.AspNetCore.Mvc;
using VodPlatform.Core.Application.Actions.Commands;
using VodPlatform.Core.Application.Actions.Queries;

namespace VodPlatform.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchedListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WatchedListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWatchedList(string userId)
            => Ok(await _mediator.Send(new GetWatchedListByIdQuery(userId)));

        [HttpGet("{userId}/items")]
        public async Task<IActionResult> GetWatchedItems(string userId)
            => Ok(await _mediator.Send(new GetWatchedItemsQuery(userId)));

        [HttpGet("{userId}/items/{watchedItemId}")]
        public async Task<IActionResult> GetWatchedItemById(int watchedItemId, string userId)
            => Ok(await _mediator.Send(new GetWatchedItemByIdQuery(watchedItemId, userId)));

        [HttpPost]
        public async Task<IActionResult> AddWatchedList([FromBody] AddWatchedListCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveWatchedList(string userId)
            => Ok(await _mediator.Send(new RemoveWatchedListCommand(userId)));

        [HttpPost("{userId}/add-movie")]
        public async Task<IActionResult> AddMovie([FromBody] AddWatchedItemMovieCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("{userId}/add-episode")]
        public async Task<IActionResult> AddEpisode([FromBody] AddWatchedItemEpisodeCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("{userId}/items/{watchedItemId}/end-watch")]
        public async Task<IActionResult> UpdateEndWatch([FromBody] UpdateEndWatchWatchedItemCommand command)
            => Ok(await _mediator.Send(command));
    }
}
