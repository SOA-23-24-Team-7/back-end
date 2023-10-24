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
        CreateMap<RatingResponseDto, Rating>().ReverseMap();
        CreateMap<Rating, RatingWithUserDto>()
            .ConstructUsing(src => new RatingWithUserDto { Id = src.Id, UserId = src.UserId, Grade = src.Grade, Comment = src.Comment, UserName = src.User.Username });
        CreateMap<RatingCreateDto, Rating>().ReverseMap();
        CreateMap<RatingUpdateDto, Rating>().ReverseMap();
    }
}