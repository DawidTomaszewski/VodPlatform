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
    internal class WatchProfile : Profile
    {
        public WatchProfile()
        {
            CreateMap<Watchlist, WatchlistDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<WatchItem, WatchItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WatchlistId, opt => opt.MapFrom(src => src.WatchlistId))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dest => dest.SeriesGroupId, opt => opt.MapFrom(src => src.SeriesGroupId));
        }
    }
}
