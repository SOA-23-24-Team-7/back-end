using AutoMapper;
using Explorer.Stakeholders.API.Dtos.TouristEquipment;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<TouristEquipmentResponseDto, TouristEquipment>().ReverseMap();
        CreateMap<TouristEquipmentCreateDto, TouristEquipment>().ReverseMap();
        CreateMap<TouristEquipmentUpdateDto, TouristEquipment>().ReverseMap();
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
            .ConstructUsing(i => new ClubInvitationCreatedDto() { Id = i.Id, ClubId = i.ClubId, TouristId = i.TouristId});

        CreateMap<ClubInvitationWithClubAndOwnerName, ClubInvitation>().ReverseMap()
            .ConstructUsing(invitation => new ClubInvitationWithClubAndOwnerName() { Id = invitation.Id, ClubName = invitation.Club.Name, OwnerUsername = invitation.Club.Owner.Username });

        CreateMap<ClubResponseDto, Club>().ReverseMap();
        CreateMap<Club, ClubResponseWithOwnerDto>()
            .ConstructUsing(src => new ClubResponseWithOwnerDto { Id = src.Id, OwnerId = src.OwnerId, Username = src.Owner.Username, Name = src.Name, Description = src.Description, Image = src.Image });
        CreateMap<ClubCreateDto, Club>().ReverseMap();
        CreateMap<PersonResponseDto, Person>().ReverseMap();
        CreateMap<PersonUpdateDto, Person>().ReverseMap();
        CreateMap<UserResponseDto, User>().ReverseMap();
        CreateMap<RatingResponseDto, Rating>().ReverseMap();
        CreateMap<Rating, RatingWithUserDto>()
            .ConstructUsing(src => new RatingWithUserDto { Id = src.Id, UserId = src.UserId, Grade = src.Grade, Comment = src.Comment, UserName = src.User.Username });
        CreateMap<RatingCreateDto, Rating>().ReverseMap();
        CreateMap<RatingUpdateDto, Rating>().ReverseMap();
        CreateMap<TourPreferenceResponseDto, TourPreference>().ReverseMap();
        CreateMap<TourPreferenceCreateDto, TourPreference>().ReverseMap();
        CreateMap<TourPreferenceUpdateDto, TourPreference>().ReverseMap();
    }
}