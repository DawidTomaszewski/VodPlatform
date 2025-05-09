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
    public record AddEpisodeCommand(int GroupId, int SeasonNumber, int EpisodeNumber, int Duration) : IRequest<int>;
    public record RemoveEpisodeByIdCommand(int GroupId, int EpisodeId) : IRequest<bool>;

    public class AddEpisodeCommandHandler : IRequestHandler<AddEpisodeCommand, int>
    {
        private readonly ISeriesGroupRepository _repository;

        public AddEpisodeCommandHandler(ISeriesGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddEpisodeCommand request, CancellationToken cancellationToken)
        {
            SeriesGroup group = await _repository.GetByIdAsync(request.GroupId);

            var Episode = new Episode(request.EpisodeNumber, request.SeasonNumber, new Duration(request.Duration));

            group.AddEpisode(Episode);

            await _repository.SaveChangesAsync();

            return Episode.Id;
        }
    }

    public class RemoveEpisodeByIdCommandHandler : IRequestHandler<RemoveEpisodeByIdCommand, bool>
    {
        private readonly ISeriesGroupRepository _repository;

        public RemoveEpisodeByIdCommandHandler(ISeriesGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveEpisodeByIdCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.GroupId);
            var Episode = group.GetEpisodeById(request.EpisodeId);

            if (Episode == null) return false;

            group.RemoveEpisodeById(request.EpisodeId);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
