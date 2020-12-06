using AutoMapper;
using Dumka.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dumka.Data
{
    public class DumkaProfile : Profile
    {
        public DumkaProfile()
        {
            CreateMap<School, SchoolDto>();
            CreateMap<SchoolDto, School>()
                .ForMember(school => school.DateModified, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(school => school.DateCreated, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
