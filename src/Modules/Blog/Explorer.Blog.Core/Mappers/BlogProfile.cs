using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogResponseDto, Domain.Blog>().ReverseMap();
        CreateMap<CommentResponseDto, Comment>().ReverseMap();
        CreateMap<CommentCreateDto, Comment>().ReverseMap();
        CreateMap<CommentCreateDto, CommentResponseDto>().ReverseMap();
    }
}