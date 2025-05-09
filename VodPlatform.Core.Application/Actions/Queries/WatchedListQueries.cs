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
    public record GetWatchedListByIdQuery(string UserId) : IRequest<WatchedListDto>;

    public class GetWatchedListByIdQueryHandler : IRequestHandler<GetWatchedListByIdQuery, WatchedListDto>
    {
        private readonly IWatchedListRepository _repository;
        private readonly IMapper _mapper;

        public GetWatchedListByIdQueryHandler(IWatchedListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WatchedListDto> Handle(GetWatchedListByIdQuery request, CancellationToken cancellationToken)
        {
            var watchedList = await _repository.GetByUserIdAsync(request.UserId);

            if (watchedList == null)
                throw new InvalidOperationException($"WatchedList with ID {request.UserId} not found.");

            return _mapper.Map<WatchedListDto>(watchedList);
        }
    }
}
