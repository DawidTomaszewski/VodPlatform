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
    public record GetMovieByIdQuery(int GroupId, int MovieId) : IRequest<MovieDto>;
    public record GetMoviesQuery(int GroupId) : IRequest<List<MovieDto>>;

    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieDto>
    {
        private readonly IMovieGroupRepository _repository;
        private readonly IMapper _mapper;

        public GetMovieByIdQueryHandler(IMovieGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MovieDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.GroupId);
            if (group == null)
                throw new InvalidOperationException($"Movie group with ID {request.GroupId} not found.");

            var movie = group.GetMovieById(request.MovieId);
            if (movie == null)
                throw new InvalidOperationException($"Movie with ID {request.MovieId} not found in group {request.GroupId}.");

            return _mapper.Map<MovieDto>(movie);
        }
    }

    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, List<MovieDto>>
    {
        private readonly IMovieGroupRepository _repository;
        private readonly IMapper _mapper;

        public GetMoviesQueryHandler(IMovieGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.GroupId);
            if (group == null)
                throw new InvalidOperationException($"Movie group with ID {request.GroupId} not found.");

            return _mapper.Map<List<MovieDto>>(group.GetMovies());
        }
    }
}
