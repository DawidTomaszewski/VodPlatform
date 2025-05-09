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
    public class SeriesGroupProfile : Profile
    {
        public SeriesGroupProfile()
        {
            CreateMap<SeriesGroup, SeriesGroupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.GetTitle().ToString()))
                .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.GetCategories()))
                .ForMember(dest => dest.TotalDuration, opt => opt.MapFrom(src => src.GetTotalDuration().ToTotalSeconds()));

            CreateMap<Episode, EpisodeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SeriesGroupId, opt => opt.MapFrom(src => src.SeriesGroupId))
                .ForMember(dest => dest.EpisodeNumber, opt => opt.MapFrom(src => src.EpisodeNumber))
                .ForMember(dest => dest.SeasonNumber, opt => opt.MapFrom(src => src.SeasonNumber))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.ToTotalSeconds()));
        }
    }
}
