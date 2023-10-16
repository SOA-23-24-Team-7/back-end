using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.UseCases
{
    public class CommentService : CrudService<CommentDto, Comment>, ICommentService
    {
        public CommentService(ICrudRepository<Comment> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
