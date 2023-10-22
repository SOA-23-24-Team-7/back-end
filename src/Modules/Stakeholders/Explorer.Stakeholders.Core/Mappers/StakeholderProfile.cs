using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<PersonResponseDto, Person>().ReverseMap();
        CreateMap<PersonUpdateDto, Person>().ReverseMap();
        CreateMap<UserResponseDto, User>().ReverseMap();
    }
}