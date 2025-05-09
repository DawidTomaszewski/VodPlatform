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
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<MovieGroup, MovieGroupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.GetTitle().ToString()))
                .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.GetCategories()))
                .ForMember(dest => dest.TotalDuration, opt => opt.MapFrom(src => src.GetTotalDuration().ToTotalSeconds()));


            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MovieGroupId, opt => opt.MapFrom(src => src.GetTitle()))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.GetTitle()))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.ToTotalSeconds()));
        }
    }
}
