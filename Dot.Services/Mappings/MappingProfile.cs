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
            CreateMap<User, UserVm>().ReverseMap();

        }
    }
}
