using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristEquipment;
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

        CreateMap<TourResponseDto, Tour>().ReverseMap();
        CreateMap<TourCreateDto, Tour>().ReverseMap();
        CreateMap<TourUpdateDto, Tour>().ReverseMap();
        CreateMap<TourResponseDto, Domain.Tour>().ReverseMap().ForMember(x => x.KeyPoints, opt => opt.MapFrom(src => src.KeyPoints));

        CreateMap<FacilityResponseDto, Facility>().ReverseMap();
        CreateMap<FacilityCreateDto, Facility>().ReverseMap();
        CreateMap<FacilityUpdateDto, Facility>().ReverseMap();

        CreateMap<KeyPointDto, KeyPoint>().ReverseMap();

        CreateMap<PreferenceResponseDto, Preference>().ReverseMap();
        CreateMap<PreferenceCreateDto, Preference>().ReverseMap();
        CreateMap<PreferenceUpdateDto, Preference>().ReverseMap();




        CreateMap<TouristEquipmentResponseDto, TouristEquipment>().ReverseMap();
        CreateMap<TouristEquipmentCreateDto, TouristEquipment>().ReverseMap();
        CreateMap<TouristEquipmentUpdateDto, TouristEquipment>().ReverseMap();

        CreateMap<PublicKeyPointRequestCreateDto, PublicKeyPointRequest>().ReverseMap() ;
        CreateMap<PublicKeyPointRequestResponseDto, PublicKeyPointRequest>().ReverseMap();
        //CreateMap<PublicKeyPointRequestResponseDto, Domain.PublicKeyPointRequest>().ReverseMap().ForMember(x => x.KeyPoint, opt => opt.MapFrom(src => src.KeyPoint));
        CreateMap<PublicKeyPointRequestUpdateDto, PublicKeyPointRequest>().ReverseMap();

    }
}