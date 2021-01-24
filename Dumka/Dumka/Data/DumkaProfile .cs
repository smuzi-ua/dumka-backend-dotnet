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

            CreateMap<Proposal, ProposalInfoDto>()
                .ForMember(proposalDto => proposalDto.UserNickname, opt => opt.MapFrom(src => src.User.Nickname))
                .ForMember(proposalDto => proposalDto.Stage, opt => opt.MapFrom(src => src.Stage.Name))
                .ForMember(proposalDto => proposalDto.Deadline, opt => opt.MapFrom(src => src.Deadline.Name))
                .ForMember(proposalDto => proposalDto.LikesCount, opt => opt.MapFrom(src => src.ProposalLikes.Count(_ => _.FeedbackId == 1)))
                .ForMember(proposalDto => proposalDto.DislikesCount, opt => opt.MapFrom(src => src.ProposalLikes.Count(_ => _.FeedbackId == 2)))
                .ForMember(proposalDto => proposalDto.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count()))
                .ForMember(proposalDto => proposalDto.Comments, opt => opt.MapFrom((src, dest, destMember, cntx) =>
                {
                    return (cntx.Items[DumkaAutomapperConstants.IsForDisplayingInList] as bool? == true) ? null : src.Comments;
                }))
                .ForMember(proposalDto => proposalDto.IsLikedByCurrentUser, opt => opt.MapFrom((src, dest, destMember, cntx) =>
                {
                    int? currentUserId = cntx.Items[DumkaAutomapperConstants.CurrentUserId] as int?;
                    return src.ProposalLikes.Any(_ => _.UserId == currentUserId && _.FeedbackId == 1);
                }))
                .ForMember(proposalDto => proposalDto.IsDislikedByCurrentUser, opt => opt.MapFrom((src, dest, destMember, cntx) =>
                {
                    int? currentUserId = cntx.Items[DumkaAutomapperConstants.CurrentUserId] as int?;
                    return src.ProposalLikes.Any(_ => _.UserId == currentUserId && _.FeedbackId == 2);
                }));

            CreateMap<ProposalDto, Proposal>()
                .ForMember(proposal => proposal.DateModified, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(proposal => proposal.UserId, opt => opt.MapFrom((src, dest, destMember, cntx) =>
                {
                    return (int)cntx.Items[DumkaAutomapperConstants.CurrentUserId];
                }));

            CreateMap<Comment, CommentInfoDto>()
                .ForMember(proposalDto => proposalDto.UserNickname, opt => opt.MapFrom(src => src.User.Nickname))
                .ForMember(proposalDto => proposalDto.LikesCount, opt => opt.MapFrom(src => src.CommentLikes.Count(_ => _.FeedbackId == 1)))
                .ForMember(proposalDto => proposalDto.DislikesCount, opt => opt.MapFrom(src => src.CommentLikes.Count(_ => _.FeedbackId == 2)))
                .ForMember(proposalDto => proposalDto.IsLikedByCurrentUser, opt => opt.MapFrom((src, dest, destMember, cntx) =>
                {
                    int? currentUserId = cntx.Items[DumkaAutomapperConstants.CurrentUserId] as int?;
                    return src.CommentLikes.Any(_ => _.UserId == currentUserId && _.FeedbackId == 1);
                }))
                .ForMember(proposalDto => proposalDto.IsDislikedByCurrentUser, opt => opt.MapFrom((src, dest, destMember, cntx) =>
                {
                    int? currentUserId = cntx.Items[DumkaAutomapperConstants.CurrentUserId] as int?;
                    return src.CommentLikes.Any(_ => _.UserId == currentUserId && _.FeedbackId == 2);
                }));

            CreateMap<CommentDto, Comment>()
                .ForMember(comment => comment.DateModified, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(comment => comment.UserId, opt => opt.MapFrom((src, dest, destMember, cntx) =>
                {
                    return (int)cntx.Items[DumkaAutomapperConstants.CurrentUserId];
                }));
        }
    }
}
