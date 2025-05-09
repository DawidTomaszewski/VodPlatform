using MediatR;
using Microsoft.AspNetCore.Mvc;
using VodPlatform.Core.Application.Actions.Commands;
using VodPlatform.Core.Application.Actions.Queries;

namespace VodPlatform.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovieGroup([FromBody] AddMovieGroupCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMovieGroup(int id)
            => Ok(await _mediator.Send(new RemoveMovieGroupCommand(id)));

        [HttpPost("{groupId}/movie")]
        public async Task<IActionResult> AddMovie(int groupId, [FromBody] AddMovieCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{groupId}/movie/{movieId}")]
        public async Task<IActionResult> RemoveMovie(int groupId, int movieId)
            => Ok(await _mediator.Send(new RemoveMovieByIdCommand(groupId, movieId)));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _mediator.Send(new GetMovieGroupByIdQuery(id)));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllMovieGroupsQuery()));
    }

}
