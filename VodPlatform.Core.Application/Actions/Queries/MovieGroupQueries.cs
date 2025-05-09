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
    public record GetMovieGroupByIdQuery(int Id) : IRequest<MovieGroupDto>;
    public record GetAllMovieGroupsQuery() : IRequest<List<MovieGroupDto>>;


    public class GetMovieGroupByIdQueryHandler : IRequestHandler<GetMovieGroupByIdQuery, MovieGroupDto>
    {
        private readonly IMovieGroupRepository _repository;
        private readonly IMapper _mapper;

        public GetMovieGroupByIdQueryHandler(IMovieGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MovieGroupDto> Handle(GetMovieGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.Id);

            if (group == null)
                throw new InvalidOperationException($"MovieGroup with ID {request.Id} not found.");

            return _mapper.Map<MovieGroupDto>(group);
        }
    }

    public class GetAllMovieGroupsQueryHandler : IRequestHandler<GetAllMovieGroupsQuery, List<MovieGroupDto>>
    {
        private readonly IMovieGroupRepository _repository;
        private readonly IMapper _mapper;

        public GetAllMovieGroupsQueryHandler(IMovieGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MovieGroupDto>> Handle(GetAllMovieGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _repository.GetAllAsync();
            return _mapper.Map<List<MovieGroupDto>>(groups);
        }
    }
}
