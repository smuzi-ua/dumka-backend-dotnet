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

            CreateMap<User, UserInfoDto>()
                .ForMember(userDto => userDto.School, opt => opt.MapFrom(src => src.School.Name))
                .ForMember(userDto => userDto.UserType, opt => opt.MapFrom(src => src.UserType.Name))
                .ForMember(userDto => userDto.ProposalsCount, opt => opt.MapFrom(src => src.Proposals.Count()))
                .ForMember(userDto => userDto.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count()));
        }
    }
}
