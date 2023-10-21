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
        CreateMap<ReviewCreateDto, Review>().ReverseMap();
        CreateMap<ReviewUpdateDto, Review>().ReverseMap();
        CreateMap<ReviewResponseDto, Review>().ReverseMap();
    }
}