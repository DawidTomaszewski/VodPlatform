using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VodPlatform.Core.Application.DTOs;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Repositories;

namespace VodPlatform.Core.Application.Actions.Queries
{
    public record GetWatchItemByIdQuery(int WatchItemId, string UserId) : IRequest<WatchItemDto>;
    public record GetWatchItemsQuery(string UserId) : IRequest<List<WatchItemDto>>;

    public class GetWatchItemByIdQueryHandler : IRequestHandler<GetWatchItemByIdQuery, WatchItemDto>
    {
        private readonly IWatchlistRepository _repository;
        private readonly IMapper _mapper;

        public GetWatchItemByIdQueryHandler(IWatchlistRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WatchItemDto> Handle(GetWatchItemByIdQuery request, CancellationToken cancellationToken)
        {
            var watchedlist = await _repository.GetByUserIdAsync(request.UserId);

            var WatchItem = watchedlist.GetWatchItemById(request.WatchItemId);

            return _mapper.Map<WatchItemDto>(WatchItem);
        }
    }


    public class GetWatchItemsQueryHandler : IRequestHandler<GetWatchItemsQuery, List<WatchItemDto>>
    {
        private readonly IWatchlistRepository _repository;
        private readonly IMapper _mapper;

        public GetWatchItemsQueryHandler(IWatchlistRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<WatchItemDto>> Handle(GetWatchItemsQuery request, CancellationToken cancellationToken)
        {
            var watchedlist = await _repository.GetByUserIdAsync(request.UserId);
            if (watchedlist != null)
            {
                var WatchItemlist = watchedlist.GetWatchItems();
                return _mapper.Map<List<WatchItemDto>>(WatchItemlist);

            }
            else
            {
                return new List<WatchItemDto>();
            }
        }
    }
}
