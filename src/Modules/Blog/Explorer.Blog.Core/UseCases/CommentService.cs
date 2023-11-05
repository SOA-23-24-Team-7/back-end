using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System.Xml.Linq;

namespace Explorer.Blog.Core.UseCases
{
    public class CommentService : CrudService<CommentResponseDto, Comment>, ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly ICrudRepository<Domain.Blog> _blogRepository;

        public CommentService(ICrudRepository<Comment> repository, ICommentRepository commentRepository, ICrudRepository<Domain.Blog> blogRepository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _blogRepository = blogRepository;
        }

        public Result<PagedResult<CommentResponseDto>> GetPagedByBlogId(int page, int pageSize, long blogId)
        {
            return MapToDto<CommentResponseDto>(_commentRepository.GetPagedByBlogId(page, pageSize, blogId));
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
