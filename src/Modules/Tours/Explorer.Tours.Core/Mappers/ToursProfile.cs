using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristEquipment;
using Explorer.Tours.API.Dtos.TouristPosition;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;

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
        CreateMap<TourUpdateDto, Tour>().ReverseMap().ForMember(dest => dest.Durations, opt => opt.MapFrom(src => src.Durations.Select(d => new TourDuration(d.Duration, (Domain.Tours.TransportType)d.TransportType))));
        CreateMap<TourResponseDto, Tour>().ReverseMap().ForMember(dest => dest.Durations, opt => opt.MapFrom(src => src.Durations.Select(d => new TourDuration(d.Duration, (Domain.Tours.TransportType)d.TransportType))));

        CreateMap<FacilityResponseDto, Facility>().ReverseMap();
        CreateMap<FacilityCreateDto, Facility>().ReverseMap();
        CreateMap<FacilityUpdateDto, Facility>().ReverseMap();

        CreateMap<KeyPointResponseDto, KeyPoint>().ReverseMap();
        CreateMap<KeyPointCreateDto, KeyPoint>().ReverseMap();
        CreateMap<KeyPointUpdateDto, KeyPoint>().ReverseMap();

        CreateMap<PreferenceResponseDto, Preference>().ReverseMap();
        CreateMap<PreferenceCreateDto, Preference>().ReverseMap();
        CreateMap<PreferenceUpdateDto, Preference>().ReverseMap();

        CreateMap<TouristEquipmentResponseDto, TouristEquipment>().ReverseMap();
        CreateMap<TouristEquipmentCreateDto, TouristEquipment>().ReverseMap();
        CreateMap<TouristEquipmentUpdateDto, TouristEquipment>().ReverseMap();

        CreateMap<TouristPositionResponseDto, TouristPosition>().ReverseMap();
        CreateMap<TouristPositionCreateDto, TouristPosition>().ReverseMap();
        CreateMap<TouristPositionUpdateDto, TouristPosition>().ReverseMap();

        CreateMap<PublicKeyPointRequestCreateDto, PublicKeyPointRequest>().ReverseMap();
        CreateMap<PublicKeyPointRequestResponseDto, PublicKeyPointRequest>().ReverseMap();
        //CreateMap<PublicKeyPointRequestResponseDto, Domain.PublicKeyPointRequest>().ReverseMap().ForMember(x => x.KeyPoint, opt => opt.MapFrom(src => src.KeyPoint));
        CreateMap<PublicKeyPointRequestUpdateDto, PublicKeyPointRequest>().ReverseMap();

        CreateMap<PublicFacilityRequestCreateDto, PublicFacilityRequest>().ReverseMap();
        CreateMap<PublicFacilityRequestResponseDto, PublicFacilityRequest>().ReverseMap();
        //CreateMap<PublicKeyPointRequestResponseDto, Domain.PublicKeyPointRequest>().ReverseMap().ForMember(x => x.KeyPoint, opt => opt.MapFrom(src => src.KeyPoint));
        CreateMap<PublicFacilityRequestUpdateDto, PublicFacilityRequest>().ReverseMap();

        CreateMap<TourDurationResponseDto, TourDuration>().ReverseMap(); 
        CreateMap<TourDurationUpdateDto, TourDuration>().ReverseMap(); 

        CreateMap<PublicFacilityNotificationResponseDto, PublicFacilityNotification>().ReverseMap();
        CreateMap<PublicFacilityNotificationCreateDto, PublicFacilityNotification>().ReverseMap();

        CreateMap<PublicKeyPointNotificationResponseDto, PublicKeyPointNotification>().ReverseMap();
        CreateMap<PublicKeyPointNotificationCreateDto, PublicKeyPointNotification>().ReverseMap();

        CreateMap<PublicKeyPointResponseDto, PublicKeyPoint>().ReverseMap();
        CreateMap<PublicKeyPointCreateDto, PublicKeyPoint>().ReverseMap();
    }
}