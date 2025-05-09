using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Core.Application.Actions.Commands
{
    public record AddWatchlistCommand(string UserId) : IRequest<int>;
    public record RemoveWatchlistCommand(string UserId) : IRequest<bool>;

    public class AddWatchlistCommandHandler : IRequestHandler<AddWatchlistCommand, int>
    {
        private readonly IWatchlistRepository _repository;

        public AddWatchlistCommandHandler(IWatchlistRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddWatchlistCommand request, CancellationToken cancellationToken)
        {
            var watchlist = new Watchlist(request.UserId);

            await _repository.AddAsync(watchlist);
            await _repository.SaveChangesAsync();

            return watchlist.Id;
        }
    }


    public class RemoveWatchlistCommandHandler : IRequestHandler<RemoveWatchlistCommand, bool>
    {
        private readonly IWatchlistRepository _repository;

        public RemoveWatchlistCommandHandler(IWatchlistRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveWatchlistCommand request, CancellationToken cancellationToken)
        {
            var watchlist = await _repository.GetByUserIdAsync(request.UserId);
            await _repository.RemoveAsync(watchlist);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
