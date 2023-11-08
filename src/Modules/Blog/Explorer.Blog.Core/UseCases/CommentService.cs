using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class CommentService : CrudService<CommentResponseDto, Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IInternalUserService _internalUserService;

        public CommentService(ICrudRepository<Comment> repository, ICommentRepository commentRepository, ICrudRepository<Domain.Blog> blogRepository, IMapper mapper, IInternalUserService internalUserService) : base(repository, mapper)
        {
            _commentRepository = commentRepository;
            _internalUserService = internalUserService;
        }

        public Result<PagedResult<CommentResponseDto>> GetPagedByBlogId(int page, int pageSize, long blogId)
        {
            var pagedComments = _commentRepository.GetPagedByBlogId(page, pageSize, blogId);
            var result = MapToDto<CommentResponseDto>(pagedComments);
            foreach (var comment in result.Value.Results)
            {
                var user = _internalUserService.Get(comment.AuthorId).Value;
                comment.Author = user;
            }
            return result;
        }

        public Result<CommentResponseDto> UpdateComment(CommentUpdateDto commentData)
        {
            try
            {
                var comment = CrudRepository.Get(commentData.Id);
                comment.UpdateText(commentData.Text);
                var result = CrudRepository.Update(comment);
                var dtoResult = MapToDto<CommentResponseDto>(result);
                dtoResult.Author = _internalUserService.Get(result.AuthorId).Value;
                return dtoResult;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
