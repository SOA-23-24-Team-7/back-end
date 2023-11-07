using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<ClubJoinRequestSendDto, ClubJoinRequest>()
            .ConstructUsing(src => new ClubJoinRequest(src.TouristId, src.ClubId, DateTime.Now, ClubJoinRequestStatus.Pending));

        CreateMap<ClubJoinRequest, ClubJoinRequestCreatedDto>()
            .ConstructUsing(src => new ClubJoinRequestCreatedDto { Id = src.Id, TouristId = src.TouristId, ClubId = src.ClubId, RequestedAt = src.RequestedAt, Status = src.GetPrimaryStatusName() });

        CreateMap<ClubJoinRequest, ClubJoinRequestByTouristDto>()
            .ConstructUsing(src => new ClubJoinRequestByTouristDto { Id = src.Id, ClubId = src.ClubId, ClubName = src.Club.Name, RequestedAt = src.RequestedAt, Status = src.GetPrimaryStatusName() });

        CreateMap<ClubJoinRequest, ClubJoinRequestByClubDto>()
            .ConstructUsing(src => new ClubJoinRequestByClubDto { Id = src.Id, TouristId = src.Tourist.Id, TouristName = src.Tourist.Username, RequestedAt = src.RequestedAt, Status = src.GetPrimaryStatusName() });

        CreateMap<ClubInvitation, ClubInvitationDto>().ReverseMap()
            .ConstructUsing(dto => new ClubInvitation(dto.ClubId, dto.TouristId));

        CreateMap<ClubInvitationCreatedDto, ClubInvitation>().ReverseMap()
            .ConstructUsing(i => new ClubInvitationCreatedDto() { Id = i.Id, ClubId = i.ClubId, TouristId = i.TouristId });

        CreateMap<ClubInvitationWithClubAndOwnerName, ClubInvitation>().ReverseMap()
            .ConstructUsing(invitation => new ClubInvitationWithClubAndOwnerName() { Id = invitation.Id, ClubName = invitation.Club.Name, OwnerUsername = invitation.Club.Owner.Username });

        CreateMap<ClubResponseDto, Club>().ReverseMap();
        CreateMap<Club, ClubResponseWithOwnerDto>()
            .ConstructUsing(src => new ClubResponseWithOwnerDto { Id = src.Id, OwnerId = src.OwnerId, Username = src.Owner.Username, Name = src.Name, Description = src.Description, Image = src.Image });
        CreateMap<ClubCreateDto, Club>().ReverseMap();
        CreateMap<Person, PersonResponseDto>().ConstructUsing(src => new PersonResponseDto
        {
            Id = src.Id,
            UserId = src.UserId,
            Name = src.Name,
            Surname = src.Surname,
            Email = src.Email,
            Bio = src.Bio,
            Motto = src.Motto,

        }).ForMember(x => x.User, opt => opt.MapFrom(src => src.User));
        CreateMap<Person, PersonUpdateDto>().ConstructUsing(src => new PersonUpdateDto
        {
            Id = src.Id,
            UserId = src.UserId,
            Name = src.Name,
            Surname = src.Surname,
            ProfilePicture = src.User.ProfilePicture,
            Bio = src.Bio,
            Motto = src.Motto
        });
        CreateMap<Follower, FollowerDto>().ConstructUsing(src => new FollowerDto { FollowedById = src.FollowedById, UserId = src.UserId, Id = src.Id, FollowedByUserName = src.FollowedBy.Username });
        CreateMap<UserResponseDto, User>().ReverseMap().ForMember(x => x.Followers, opt => opt.MapFrom(src => src.Followers));
        CreateMap<RatingResponseDto, Rating>().ReverseMap();
        CreateMap<Rating, RatingWithUserDto>()
            .ConstructUsing(src => new RatingWithUserDto { Id = src.Id, UserId = src.UserId, Grade = src.Grade, Comment = src.Comment, UserName = src.User.Username });
        CreateMap<RatingCreateDto, Rating>().ReverseMap();
        CreateMap<RatingUpdateDto, Rating>().ReverseMap();
        CreateMap<Problem, ProblemResponseDto>().ConstructUsing(src => new ProblemResponseDto
        {
            Id = src.Id,
            Category = src.Category,
            Priority = src.Priority,
            Description = src.Description,
            DateTime = src.DateTime,
            TouristId = src.TouristId,
            TourId = src.TourId,
            IsResolved = src.IsResolved
        }).ForMember(x => x.Tourist, opt => opt.MapFrom(src => src.Tourist));
        CreateMap<ProblemCreateDto, Problem>().ReverseMap();
        CreateMap<ProblemUpdateDto, Problem>().ReverseMap();
        CreateMap<ProblemAnswerCreateDto, ProblemAnswer>().ReverseMap();
        CreateMap<ProblemAnswerResponseDto, ProblemAnswer>().ReverseMap();
        CreateMap<ProblemCommentCreateDto, ProblemComment>().ReverseMap();
        CreateMap<ProblemCommentResponseDto, ProblemComment>().ReverseMap();
        CreateMap<MessageCreateDto, Message>().ReverseMap();
        CreateMap<MessageResponseDto, Message>().ReverseMap();
        CreateMap<MessageUpdateDto, Message>().ReverseMap();
        CreateMap<Message, MessageResponseWithUsernamesDto>()
            .ConstructUsing(src => new MessageResponseWithUsernamesDto { Id = src.Id, UserReciverId = src.UserReciverId, ReciverUsername = src.UserReciver.Username, UserSenderId = src.UserSenderId, SenderUsername = src.UserSender.Username, Text = src.Text, StatusOfMessage = src.StatusOfMessage.ToString() });

    }
}