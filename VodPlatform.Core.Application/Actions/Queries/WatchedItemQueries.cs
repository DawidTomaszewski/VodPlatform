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
    public record GetWatchedItemByIdQuery(int WatchedItemId, string UserId) : IRequest<WatchedItemDto>;
    public record GetWatchedItemsQuery(string UserId) : IRequest<List<WatchedItemDto>>;

    public class GetWatchedItemByIdQueryHandler : IRequestHandler<GetWatchedItemByIdQuery, WatchedItemDto>
    {
        private readonly IWatchedListRepository _repository;
        private readonly IMapper _mapper;

        public GetWatchedItemByIdQueryHandler(IWatchedListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WatchedItemDto> Handle(GetWatchedItemByIdQuery request, CancellationToken cancellationToken)
        {
            var watchedlist = await _repository.GetByUserIdAsync(request.UserId);

            var watcheditem = watchedlist.GetWatchedItemById(request.WatchedItemId);

            return _mapper.Map<WatchedItemDto>(watcheditem);
        }
    }


    public class GetWatchedItemsQueryHandler : IRequestHandler<GetWatchedItemsQuery, List<WatchedItemDto>>
    {
        private readonly IWatchedListRepository _repository;
        private readonly IMapper _mapper;

        public GetWatchedItemsQueryHandler(IWatchedListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<WatchedItemDto>> Handle(GetWatchedItemsQuery request, CancellationToken cancellationToken)
        {
            var watchedlist = await _repository.GetByUserIdAsync(request.UserId);

            var watcheditemlist = watchedlist.GetWatchedItems();

            return _mapper.Map<List<WatchedItemDto>>(watcheditemlist);
        }
    }
}
