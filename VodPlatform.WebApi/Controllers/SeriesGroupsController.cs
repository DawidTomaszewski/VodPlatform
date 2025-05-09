using MediatR;
using Microsoft.AspNetCore.Mvc;
using VodPlatform.Core.Application.Actions.Commands;
using VodPlatform.Core.Application.Actions.Queries;

namespace VodPlatform.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeriesGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeriesGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddSeriesGroup([FromBody] AddSeriesGroupCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSeriesGroup(int id)
            => Ok(await _mediator.Send(new RemoveSeriesGroupCommand(id)));

        [HttpPost("{groupId}/episode")]
        public async Task<IActionResult> AddEpisode(int groupId, [FromBody] AddEpisodeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{groupId}/episode/{episodeId}")]
        public async Task<IActionResult> RemoveEpisode(int groupId, int episodeId)
            => Ok(await _mediator.Send(new RemoveEpisodeByIdCommand(groupId, episodeId)));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _mediator.Send(new GetSeriesGroupByIdQuery(id)));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllSeriesGroupsQuery()));
    }

}
