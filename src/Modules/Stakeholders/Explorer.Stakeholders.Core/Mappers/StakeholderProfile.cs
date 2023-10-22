using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using System.Security.Cryptography.X509Certificates;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<TourPreferenceResponseDto, TourPreference>().ReverseMap();
        CreateMap<TourPreferenceCreateDto, TourPreference>().ReverseMap();
        CreateMap<TourPreferenceUpdateDto, TourPreference>().ReverseMap();
    }
}