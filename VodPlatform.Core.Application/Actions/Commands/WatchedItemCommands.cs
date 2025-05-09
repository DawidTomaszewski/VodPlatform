using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Core.Domain.Aggregates;

namespace VodPlatform.Core.Application.Actions.Commands
{
    public record AddWatchedItemMovieCommand(int? MovieId, string UserId, int Time) : IRequest<int>;
    public record AddWatchedItemEpisodeCommand(int? EpisodeId, string UserId, int Time) : IRequest<int>;
    public record UpdateEndWatchWatchedItemCommand(int WatchedItemId, string UserId, int Time) : IRequest<int>;


    public class AddWatchedItemMovieCommandHandler : IRequestHandler<AddWatchedItemMovieCommand, int>
    {
        private readonly IWatchedListRepository _repository;

        public AddWatchedItemMovieCommandHandler(IWatchedListRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddWatchedItemMovieCommand request, CancellationToken cancellationToken)
        {
            var watchedlist = await _repository.GetByUserIdAsync(request.UserId);

            watchedlist.AddMovie(request.MovieId, request.Time);

            await _repository.SaveChangesAsync();

            return watchedlist.Id;
        }
    }


    public class AddWatchedItemEpisodeCommandHandler : IRequestHandler<AddWatchedItemEpisodeCommand, int>
    {
        private readonly IWatchedListRepository _repository;

        public AddWatchedItemEpisodeCommandHandler(IWatchedListRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddWatchedItemEpisodeCommand request, CancellationToken cancellationToken)
        {
            var watchedlist = await _repository.GetByUserIdAsync(request.UserId);

            watchedlist.AddEpisode(request.EpisodeId, request.Time);

            await _repository.SaveChangesAsync();

            return watchedlist.Id;
        }
    }


    public class UpdateEndWatchWatchedItemCommandHandler : IRequestHandler<UpdateEndWatchWatchedItemCommand, int>
    {
        private readonly IWatchedListRepository _repository;

        public UpdateEndWatchWatchedItemCommandHandler(IWatchedListRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(UpdateEndWatchWatchedItemCommand request, CancellationToken cancellationToken)
        {
            var watchedlist = await _repository.GetByUserIdAsync(request.UserId);

            var watchedtem = watchedlist.GetWatchedItemById(request.WatchedItemId);

            watchedtem.UpdateEndWatch(request.Time);

            await _repository.SaveChangesAsync();

            return watchedlist.Id;
        }
    }
}
