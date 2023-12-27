using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.TouristEquipment;
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

        CreateMap<SubscriberResponseDto, Subscriber>().ReverseMap();
        CreateMap<SubscriberCreateDto, Subscriber>().ReverseMap();

        CreateMap<ReviewCreateDto, Review>().ReverseMap();
        CreateMap<ReviewUpdateDto, Review>().ReverseMap();
        CreateMap<ReviewResponseDto, Review>().ReverseMap();

        CreateMap<CampaignResponseDto, Campaign>().ReverseMap();
        CreateMap<TourCampaignResponseDto, Campaign>().ReverseMap();

        CreateMap<TourResponseDto, Tour>().ReverseMap();
        CreateMap<TourCreateDto, Tour>().ReverseMap();
        CreateMap<TourUpdateDto, Tour>().ReverseMap().ForMember(dest => dest.Durations, opt => opt.MapFrom(src => src.Durations.Select(d => new TourDuration(d.Duration, (Domain.Tours.TransportType)d.TransportType))));
        CreateMap<TourResponseDto, Tour>().ReverseMap().ForMember(dest => dest.Durations, opt => opt.MapFrom(src => src.Durations.Select(d => new TourDuration(d.Duration, (Domain.Tours.TransportType)d.TransportType))));

        CreateMap<FacilityResponseDto, Facility>().ReverseMap();
        CreateMap<FacilityCreateDto, Facility>().ReverseMap();
        CreateMap<FacilityUpdateDto, Facility>().ReverseMap();

        CreateMap<KeyPointSecretDto, KeyPointSecret>().ReverseMap();

        CreateMap<KeyPointResponseDto, KeyPoint>().ReverseMap();
        CreateMap<KeyPointCreateDto, KeyPoint>().ForMember(dest => dest.HaveSecret, opt => opt.MapFrom(src => src.Secret != null));
        CreateMap<KeyPoint, KeyPointUpdateDto>().ReverseMap().ConstructUsing(x => new KeyPoint(x.TourId, x.Name, x.Description, x.Longitude, x.Latitude, x.LocationAddress, x.ImagePath, x.Order, null));

        CreateMap<TourExecutionSession, TourExecutionSessionResponseDto>();
        CreateMap<TourExecutionSession, TourExecutionInfoDto>().ForMember(dest => dest.TourStatus, opt => opt.MapFrom(src => src.Status));
        CreateMap<Tour, TourExecutionInfoDto>().ForMember(dest => dest.TourExecutionStatus, opt => opt.MapFrom(src => src.Status));

        CreateMap<TouristPositionResponseDto, TouristPosition>().ReverseMap();
        CreateMap<TouristPositionCreateDto, TouristPosition>().ReverseMap();
        CreateMap<TouristPositionUpdateDto, TouristPosition>().ReverseMap();


        CreateMap<PreferenceResponseDto, Preference>().ReverseMap();
        CreateMap<PreferenceCreateDto, Preference>().ReverseMap();
        CreateMap<PreferenceUpdateDto, Preference>().ReverseMap();

        CreateMap<TouristEquipmentResponseDto, TouristEquipment>().ReverseMap();
        CreateMap<TouristEquipmentCreateDto, TouristEquipment>().ReverseMap();
        CreateMap<TouristEquipmentUpdateDto, TouristEquipment>().ReverseMap();

        CreateMap<TourExecutionSessionResponseDto, TourExecutionSession>().ReverseMap();

        CreateMap<TouristPositionResponseDto, TouristPosition>().ReverseMap();
        CreateMap<TouristPositionCreateDto, TouristPosition>().ReverseMap();
        CreateMap<TouristPositionUpdateDto, TouristPosition>().ReverseMap();
        CreateMap<PublicKeyPointRequestCreateDto, PublicKeyPointRequest>().ReverseMap();
        CreateMap<PublicKeyPointRequestResponseDto, PublicKeyPointRequest>().ReverseMap();
        CreateMap<PublicKeyPointRequestUpdateDto, PublicKeyPointRequest>().ReverseMap();

        CreateMap<PublicFacilityRequestCreateDto, PublicFacilityRequest>().ReverseMap();
        CreateMap<PublicFacilityRequestResponseDto, PublicFacilityRequest>().ReverseMap();
        CreateMap<PublicFacilityRequestUpdateDto, PublicFacilityRequest>().ReverseMap();

        CreateMap<TourDurationResponseDto, TourDuration>().ReverseMap();
        CreateMap<TourDurationUpdateDto, TourDuration>().ReverseMap();

        CreateMap<PublicFacilityNotificationResponseDto, PublicFacilityNotification>().ReverseMap();
        CreateMap<PublicFacilityNotificationCreateDto, PublicFacilityNotification>().ReverseMap();

        CreateMap<PublicKeyPointNotificationResponseDto, PublicKeyPointNotification>().ReverseMap();
        CreateMap<PublicKeyPointNotificationCreateDto, PublicKeyPointNotification>().ReverseMap();

        CreateMap<PublicKeyPointResponseDto, PublicKeyPoint>().ReverseMap();
        CreateMap<PublicKeyPointCreateDto, PublicKeyPoint>().ReverseMap();

        CreateMap<LimitedTourViewResponseDto, Tour>().ReverseMap();

    }
}