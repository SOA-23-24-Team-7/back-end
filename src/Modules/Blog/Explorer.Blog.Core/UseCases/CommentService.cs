using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class CommentService : CrudService<CommentResponseDto, Comment>, ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICrudRepository<Comment> repository, ICommentRepository commentRepository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public Result<CommentResponseDto> Create(CommentRequestDto commentData, long authorId)
        {
            try
            {
                var comment = _mapper.Map<CommentResponseDto>(commentData);
                comment.AuthorId = authorId;
                comment.CreatedAt = DateTime.Now.ToUniversalTime();

                return MapToDto(CrudRepository.Create(_mapper.Map<Comment>(comment)));
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

        }

        public Result<PagedResult<CommentResponseDto>> GetPagedByBlogId(int page, int pageSize, long blogId)
        {
            return MapToDto(_commentRepository.GetPagedByBlogId(page, pageSize, blogId));
        }
    }
}
