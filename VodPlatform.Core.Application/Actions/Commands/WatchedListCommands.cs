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
    public record AddWatchedListCommand(string UserId) : IRequest<int>;
    public record RemoveWatchedListCommand(string UserId) : IRequest<bool>;

    public class AddWatchedListCommandHandler : IRequestHandler<AddWatchedListCommand, int>
    {
        private readonly IWatchedListRepository _repository;

        public AddWatchedListCommandHandler(IWatchedListRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddWatchedListCommand request, CancellationToken cancellationToken)
        {
            var watchedList = new WatchedList(request.UserId);

            await _repository.AddAsync(watchedList);
            await _repository.SaveChangesAsync();

            return watchedList.Id;
        }
    }


    public class RemoveWatchedListCommandHandler : IRequestHandler<RemoveWatchedListCommand, bool>
    {
        private readonly IWatchedListRepository _repository;

        public RemoveWatchedListCommandHandler(IWatchedListRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveWatchedListCommand request, CancellationToken cancellationToken)
        {
            var watchedlist = await _repository.GetByUserIdAsync(request.UserId);
            await _repository.RemoveAsync(watchedlist);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
