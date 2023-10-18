using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class CommentService : CrudService<CommentResponseDto, Comment>, ICommentService
    {
        private readonly IMapper _mapper;
        public CommentService(ICrudRepository<Comment> repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
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
    }
}
