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

            CreateMap<Proposal, ProposalDto>()
                .ForMember(proposalDto => proposalDto.UserNickname, opt => opt.MapFrom(src => src.User.Nickname))
                .ForMember(proposalDto => proposalDto.Stage, opt => opt.MapFrom(src => src.Stage.Name))
                .ForMember(proposalDto => proposalDto.Deadline, opt => opt.MapFrom(src => src.Deadline.Name))
                .ForMember(proposalDto => proposalDto.LikesCount, opt => opt.MapFrom(src => src.ProposalLikes.Count(_ => _.FeedbackId == 1)))
                .ForMember(proposalDto => proposalDto.DislikesCount, opt => opt.MapFrom(src => src.ProposalLikes.Count(_ => _.FeedbackId == 2)))
                .ForMember(proposalDto => proposalDto.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count()));
        }
    }
}
