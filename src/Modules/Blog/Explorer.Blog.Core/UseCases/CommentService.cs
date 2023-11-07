using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using FluentResults;
using System.Xml.Linq;

namespace Explorer.Blog.Core.UseCases
{
    public class CommentService : CrudService<CommentResponseDto, Comment>, ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly ICrudRepository<Domain.Blog> _blogRepository;
        private readonly IInternalUserService _internalUserService;

        public CommentService(ICrudRepository<Comment> repository, ICommentRepository commentRepository, ICrudRepository<Domain.Blog> blogRepository, IMapper mapper, IInternalUserService internalUserService) : base(repository, mapper)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _blogRepository = blogRepository;
            _internalUserService = internalUserService;
        }

        public Result<PagedResult<CommentResponseDto>> GetPagedByBlogId(int page, int pageSize, long blogId)
        {
            var pagedComments = _commentRepository.GetPagedByBlogId(page, pageSize, blogId);
            List<Comment> result = new List<Comment>();
            foreach (var comment in pagedComments.Results)
            {
                var username = _internalUserService.GetNameById(comment.AuthorId).Value;
                var newComment = new Comment(comment.AuthorId, comment.BlogId, comment.CreatedAt, comment.UpdatedAt, comment.Text, username);
                result.Add(newComment);
            }

            pagedComments = new PagedResult<Comment>(result, result.Count);
            return MapToDto<CommentResponseDto>(pagedComments);
        }

        public Result<CommentResponseDto> UpdateComment(CommentUpdateDto commentData)
        {
            try
            {
                var comment = CrudRepository.Get(commentData.Id);
                comment.UpdateText(commentData.Text);
                var result = CrudRepository.Update(comment);
                return MapToDto<CommentResponseDto>(result);
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
