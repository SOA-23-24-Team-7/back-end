using AutoMapper;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Encounter;

namespace Explorer.Encounters.Core.Mappers;

public class EncountersProfile : Profile
{
    public EncountersProfile()
    {
        CreateMap<EncounterCreateDto,Encounter>().ReverseMap();
        CreateMap<EncounterUpdateDto,Encounter>().ReverseMap();
        CreateMap<EncounterResponseDto,Encounter>().ReverseMap();
        CreateMap<EncounterCreateDto, EncounterResponseDto>().ReverseMap();
        CreateMap<EncounterUpdateDto, EncounterResponseDto>().ReverseMap();
        CreateMap<MiscEncounterResponseDto, MiscEncounter>().ReverseMap();
    }
}
