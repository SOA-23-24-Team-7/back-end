using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentResponseDto, Equipment>().ReverseMap();
        CreateMap<EquipmentCreateDto, Equipment>().ReverseMap();
        CreateMap<EquipmentUpdateDto, Equipment>().ReverseMap();

        CreateMap<FacilityResponseDto, Facility>().ReverseMap();
        CreateMap<FacilityCreateDto, Facility>().ReverseMap();
        CreateMap<FacilityUpdateDto, Facility>().ReverseMap();
    }
}