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
    public record AddWatchItemMovieCommand(int? MovieId, string UserId) : IRequest<int>;
    public record AddWatchItemSeriesCommand(int? SeriesId, string UserId) : IRequest<int>;
    public record RemoveItemWatchItemCommand(int WatchItemId, string UserId) : IRequest<int>;


    public class AddWatchItemMovieCommandHandler : IRequestHandler<AddWatchItemMovieCommand, int>
    {
        private readonly IWatchlistRepository _repository;

        public AddWatchItemMovieCommandHandler(IWatchlistRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddWatchItemMovieCommand request, CancellationToken cancellationToken)
        {
            var watchlist = await _repository.GetByUserIdAsync(request.UserId);

            watchlist.AddMovie(request.MovieId);

            await _repository.SaveChangesAsync();

            return watchlist.Id;
        }
    }


    public class AddWatchItemSeriesCommandHandler : IRequestHandler<AddWatchItemSeriesCommand, int>
    {
        private readonly IWatchlistRepository _repository;

        public AddWatchItemSeriesCommandHandler(IWatchlistRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddWatchItemSeriesCommand request, CancellationToken cancellationToken)
        {
            var watchlist = await _repository.GetByUserIdAsync(request.UserId);

            watchlist.AddSeries(request.SeriesId);

            await _repository.SaveChangesAsync();

            return watchlist.Id;
        }
    }


    public class RemoveItemWatchItemCommandHandler : IRequestHandler<RemoveItemWatchItemCommand, int>
    {
        private readonly IWatchlistRepository _repository;

        public RemoveItemWatchItemCommandHandler(IWatchlistRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(RemoveItemWatchItemCommand request, CancellationToken cancellationToken)
        {
            var watchlist = await _repository.GetByUserIdAsync(request.UserId);

            watchlist.RemoveItem(request.WatchItemId);

            await _repository.SaveChangesAsync();

            return watchlist.Id;
        }
    }
}
