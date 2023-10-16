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
        CreateMap<ClubJoinRequestDto, ClubJoinRequest>()
            .ConstructUsing(src => new ClubJoinRequest(null, src.TouristId, src.ClubId, DateTime.Now, ClubJoinRequestStatus.Pending)).ReverseMap();
        CreateMap<ClubDto, Club>().ReverseMap();
    }
}