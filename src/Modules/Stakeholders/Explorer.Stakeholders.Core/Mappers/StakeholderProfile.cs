using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<ClubResponseDto, Club>().ReverseMap();
        CreateMap<Club, ClubResponseWithOwnerDto>()
            .ConstructUsing(src => new ClubResponseWithOwnerDto { Id = src.Id, OwnerId = src.OwnerId, Username = src.Owner.Username, Name = src.Name, Description = src.Description, Image = src.Image });
        CreateMap<ClubCreateDto, Club>().ReverseMap();
    }
}