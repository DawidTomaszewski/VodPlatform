using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VodPlatform.Core.Application.DTOs;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Entities;

namespace VodPlatform.Core.Application.Mappings
{
    internal class WatchedProfile : Profile
    {
        public WatchedProfile()
        {
            CreateMap<WatchedList, WatchedListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<WatchedItem, WatchedItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WatchedListId, opt => opt.MapFrom(src => src.WatchedListId))
                .ForMember(dest => dest.WatchedAt, opt => opt.MapFrom(src => src.WatchedAt))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dest => dest.EpisodeId, opt => opt.MapFrom(src => src.EpisodeId))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.EndWatch.ToTotalSeconds()));
        }
    }
}
