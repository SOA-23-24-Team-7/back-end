using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogResponseDto, Domain.Blog>().ReverseMap().ForMember(x => x.Comments, opt => opt.MapFrom(src => src.Comments)).ForMember(x => x.Votes, opt => opt.MapFrom(src => src.Votes));
        CreateMap<Domain.Blog, BlogCreateDto>().ReverseMap();
        CreateMap<Domain.Blog, BlogUpdateDto>().ReverseMap();

        CreateMap<CommentResponseDto, Comment>().ReverseMap();
        CreateMap<Domain.Blog, BlogUpdateDto>().ReverseMap();
        CreateMap<Comment, CommentCreateDto>().ReverseMap().ConstructUsing(x => new Comment(x.AuthorId, x.BlogId, x.CreatedAt, null, x.Text));
        CreateMap<CommentCreateDto, CommentResponseDto>().ReverseMap();

        CreateMap<VoteResponseDto, Vote>().ReverseMap();
    }
}