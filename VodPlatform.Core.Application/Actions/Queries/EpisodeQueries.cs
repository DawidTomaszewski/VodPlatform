using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VodPlatform.Core.Application.DTOs;
using VodPlatform.Core.Domain.Repositories;

namespace VodPlatform.Core.Application.Actions.Queries
{
    public record GetEpisodeByIdQuery(int GroupId, int EpisodeId) : IRequest<EpisodeDto>;
    public record GetEpisodesQuery(int GroupId) : IRequest<List<EpisodeDto>>;

    public class GetEpisodeByIdQueryHandler : IRequestHandler<GetEpisodeByIdQuery, EpisodeDto>
    {
        private readonly ISeriesGroupRepository _repository;
        private readonly IMapper _mapper;

        public GetEpisodeByIdQueryHandler(ISeriesGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EpisodeDto> Handle(GetEpisodeByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.GroupId);
            if (group == null)
                throw new InvalidOperationException($"Episode group with ID {request.GroupId} not found.");

            var Episode = group.GetEpisodeById(request.EpisodeId);
            if (Episode == null)
                throw new InvalidOperationException($"Episode with ID {request.EpisodeId} not found in group {request.GroupId}.");

            return _mapper.Map<EpisodeDto>(Episode);
        }
    }

    public class GetEpisodesQueryHandler : IRequestHandler<GetEpisodesQuery, List<EpisodeDto>>
    {
        private readonly ISeriesGroupRepository _repository;
        private readonly IMapper _mapper;

        public GetEpisodesQueryHandler(ISeriesGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<EpisodeDto>> Handle(GetEpisodesQuery request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.GroupId);
            if (group == null)
                throw new InvalidOperationException($"Episode group with ID {request.GroupId} not found.");

            return _mapper.Map<List<EpisodeDto>>(group.GetEpisodes());
        }
    }
}
