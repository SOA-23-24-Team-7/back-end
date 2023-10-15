using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<ClubInvitation, ClubInvitationDto>().ReverseMap()
            .ConstructUsing(dto => new ClubInvitation(dto.ClubId, dto.TouristId));
        CreateMap<ClubDto, Club>().ReverseMap();
    }
}