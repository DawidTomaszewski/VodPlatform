using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VodPlatform.Core.Application.DTOs;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Repositories;

namespace VodPlatform.Core.Application.Actions.Queries
{
    public record GetWatchlistByIdQuery(string UserId) : IRequest<WatchlistDto>;

    public class GetWatchlistByIdQueryHandler : IRequestHandler<GetWatchlistByIdQuery, WatchlistDto>
    {
        private readonly IWatchlistRepository _repository;
        private readonly IMapper _mapper;

        public GetWatchlistByIdQueryHandler(IWatchlistRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WatchlistDto> Handle(GetWatchlistByIdQuery request, CancellationToken cancellationToken)
        {
            var WatchlistList = await _repository.GetByUserIdAsync(request.UserId);

            if (WatchlistList == null)
                throw new InvalidOperationException($"Watchlist with ID {request.UserId} not found.");

            return _mapper.Map<WatchlistDto>(WatchlistList);
        }
    }
}
