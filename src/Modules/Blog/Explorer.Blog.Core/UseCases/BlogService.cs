using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogResponseDto, Domain.Blog>, IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;
        public BlogService(ICrudRepository<Domain.Blog> crudRepository, IBlogRepository repository, IMapper mapper) : base(crudRepository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Result<BlogResponseDto> GetById(long id)
        {
            var entity = _repository.GetById(id);
            return MapToDto<BlogResponseDto>(entity);
        }

        public Result<PagedResult<BlogResponseDto>> GetAll(int page, int pageSize)
        {
            var entities = _repository.GetAll(page, pageSize);
            return MapToDto<BlogResponseDto>(entities);
        }

        public Result SetVote(long blogId, long userId, API.Dtos.VoteType voteType)
        {
            var domainVoteType = (Domain.VoteType)voteType; // bruh...
            try
            {
                var blog = CrudRepository.Get(blogId);
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

    }
}
