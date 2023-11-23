using AutoMapper;
using Explorer.Encounters.API.Dtos;

namespace Explorer.Encounters.Core.Mappers;

public class EncountersProfile : Profile
{
    public EncountersProfile()
    {
        CreateMap<EncounterCreateDto,Encounter.Core.Domain.Encounter>().ReverseMap();
        CreateMap<EncounterResponseDto,Encounter.Core.Domain.Encounter>().ReverseMap();
        CreateMap<EncounterCreateDto, EncounterResponseDto>().ReverseMap();
    }
}
