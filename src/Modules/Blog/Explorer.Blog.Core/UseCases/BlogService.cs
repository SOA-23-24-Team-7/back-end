using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogResponseDto, Domain.Blog>, IBlogService
    {

        private readonly IBlogRepository _repository;
        private readonly IInternalUserService _internalUserService;
        private readonly IMapper _mapper;
        public BlogService(ICrudRepository<Domain.Blog> crudRepository, IBlogRepository repository, IMapper mapper, IInternalUserService internalUserService) : base(crudRepository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _internalUserService = internalUserService;
        }

        public Result<BlogResponseDto> GetById(long id)
        {
            var entity = _repository.GetById(id);
            return MapToDto<BlogResponseDto>(entity);
        }

        public Result<PagedResult<BlogResponseDto>> GetAll(int page, int pageSize)
        {
            var entities = _repository.GetAll(page, pageSize);
            var result = MapToDto<BlogResponseDto>(entities);
            foreach (var blog in result.Value.Results)
            {
                var user = _internalUserService.Get(blog.AuthorId).Value;
                blog.Author = user;
            }
            return result;
        }

        public Result<BlogResponseDto> UpdateBlog(BlogUpdateDto blogUpdateDto)
        {
            try
            {
                var blog = _repository.GetById(blogUpdateDto.Id);
                blog.UpdateBlog(blogUpdateDto.Title, blogUpdateDto.Description, (Domain.BlogStatus)blogUpdateDto.Status);
                CrudRepository.Update(blog);

                return MapToDto<BlogResponseDto>(blog);

            }
            catch
            {

                return Result.Fail(FailureCode.NotFound);
            }
        }

        public Result SetVote(long blogId, long userId, API.Dtos.VoteType voteType)
        {
            var domainVoteType = (Domain.VoteType)voteType; // bruh...
            try
            {
                var blog = _repository.GetById(blogId);
                blog.SetVote(userId, domainVoteType);
                CrudRepository.Update(blog);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public bool IsBlogClosed(long blogId)
        {
            var blog = CrudRepository.Get(blogId);

            if (blog.Status == Domain.BlogStatus.Closed)
            {
                return true;
            }
            return false;
        }

        public Result<PagedResult<BlogResponseDto>> GetAllFromBlogs(int page, int pageSize, long clubId)
        {
            var entities = _repository.GetAllFromClub(page, pageSize, clubId);
            var result = MapToDto<BlogResponseDto>(entities);
            foreach (var blog in result.Value.Results)
            {
                var user = _internalUserService.Get(blog.AuthorId).Value;
                blog.Author = user;
            }
            return result;
        }
    }
}
