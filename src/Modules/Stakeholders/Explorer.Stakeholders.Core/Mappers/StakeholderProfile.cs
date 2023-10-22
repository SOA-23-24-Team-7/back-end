using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using System.Reflection;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<ClubJoinRequestSendDto, ClubJoinRequest>()
            .ConstructUsing(src => new ClubJoinRequest(src.TouristId, src.ClubId, DateTime.Now, ClubJoinRequestStatus.Pending)).ReverseMap();

        CreateMap<ClubJoinRequest, ClubJoinRequestByTouristDto>()
            .ConstructUsing(src => new ClubJoinRequestByTouristDto { Id = src.Id, ClubId = src.ClubId, ClubName = src.Club.Name, RequestedAt = src.RequestedAt, Status = src.GetPrimaryStatusName() });

        CreateMap<ClubJoinRequest, ClubJoinRequestByClubDto>()
            .ConstructUsing(src => new ClubJoinRequestByClubDto { Id = src.Id, TouristId = src.Tourist.UserId, TouristName = src.Tourist.Name + " " + src.Tourist.Surname, RequestedAt = src.RequestedAt, Status = src.GetPrimaryStatusName() });

        CreateMap<ClubInvitation, ClubInvitationDto>().ReverseMap()
            .ConstructUsing(dto => new ClubInvitation(dto.ClubId, dto.TouristId));

        CreateMap<ClubDto, Club>().ReverseMap();

        CreateMap<PersonDto, Person>().ReverseMap();
        CreateMap<ClubResponseDto, Club>().ReverseMap();
        CreateMap<Club, ClubResponseWithOwnerDto>()
            .ConstructUsing(src => new ClubResponseWithOwnerDto { Id = src.Id, OwnerId = src.OwnerId, Username = src.Owner.Username, Name = src.Name, Description = src.Description, Image = src.Image });
        CreateMap<ClubCreateDto, Club>().ReverseMap();
    }
}