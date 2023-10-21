using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {


        CreateMap<ProblemResponseDto, Problem>().ReverseMap();
        CreateMap<ProblemCreateDto, Problem>().ReverseMap();
        CreateMap<ProblemUpdateDto, Problem>().ReverseMap();
        CreateMap<EquipmentResponseDto, Equipment>().ReverseMap();
        CreateMap<EquipmentCreateDto, Equipment>().ReverseMap();
        CreateMap<EquipmentUpdateDto, Equipment>().ReverseMap();

    }
}