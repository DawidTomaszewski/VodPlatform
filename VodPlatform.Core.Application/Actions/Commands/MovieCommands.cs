using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Core.Application.Actions.Commands
{
    public record AddMovieCommand(int GroupId, string Title, int Duration) : IRequest<int>;
    public record RemoveMovieByIdCommand(int GroupId, int MovieId) : IRequest<bool>;

    public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, int>
    {
        private readonly IMovieGroupRepository _repository;

        public AddMovieCommandHandler(IMovieGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            MovieGroup group = await _repository.GetByIdAsync(request.GroupId);

            var movie = new Movie(request.Title, new Duration(request.Duration));

            group.AddMovie(movie);

            await _repository.SaveChangesAsync();

            return movie.Id;
        }
    }

    public class RemoveMovieByIdCommandHandler : IRequestHandler<RemoveMovieByIdCommand, bool>
    {
        private readonly IMovieGroupRepository _repository;

        public RemoveMovieByIdCommandHandler(IMovieGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveMovieByIdCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.GroupId);
            var movie = group.GetMovieById(request.MovieId);

            if (movie == null) return false; 

            group.RemoveMovieById(request.MovieId);
            await _repository.SaveChangesAsync();

            return true; 
        }
    }
}
