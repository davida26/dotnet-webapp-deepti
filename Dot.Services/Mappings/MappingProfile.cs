using AutoMapper;
using Dot.Services.Models;
using Dot.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dot.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserVm>()
                .ForMember(dest =>
                                    dest.AvatarUrl,
                                    opt => opt.MapFrom(src => src.Avatar_Url))
                .ForMember(dest =>
                                    dest.Followers,
                                    opt => opt.MapFrom(src => src.Followers))
                .ReverseMap();

            CreateMap<Follower, FollowerVm>().ReverseMap();

            CreateMap<Favorite, FavoriteVm>().ReverseMap();
        }
    }
}
