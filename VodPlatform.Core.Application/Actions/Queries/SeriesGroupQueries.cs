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
    public record GetSeriesGroupByIdQuery(int Id) : IRequest<SeriesGroupDto>;
    public record GetAllSeriesGroupsQuery() : IRequest<List<SeriesGroupDto>>;


    public class GetSeriesGroupByIdQueryHandler : IRequestHandler<GetSeriesGroupByIdQuery, SeriesGroupDto>
    {
        private readonly ISeriesGroupRepository _repository;
        private readonly IMapper _mapper;

        public GetSeriesGroupByIdQueryHandler(ISeriesGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SeriesGroupDto> Handle(GetSeriesGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.Id);

            if (group == null)
                throw new InvalidOperationException($"SeriesGroup with ID {request.Id} not found.");

            return _mapper.Map<SeriesGroupDto>(group);
        }
    }

    public class GetAllSeriesGroupsQueryHandler : IRequestHandler<GetAllSeriesGroupsQuery, List<SeriesGroupDto>>
    {
        private readonly ISeriesGroupRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSeriesGroupsQueryHandler(ISeriesGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SeriesGroupDto>> Handle(GetAllSeriesGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _repository.GetAllAsync();
            return _mapper.Map<List<SeriesGroupDto>>(groups);
        }
    }
}
