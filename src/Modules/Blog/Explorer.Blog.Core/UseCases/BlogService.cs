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
        private readonly IVoteRepository _voteRepository;
        private readonly ICrudRepository<Vote> _voteCrudRepository;
        private readonly IMapper _mapper;
        public BlogService(ICrudRepository<Domain.Blog> crudRepository, IBlogRepository repository, ICrudRepository<Vote> voteCrudRepository, IVoteRepository voteRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _repository = repository;
            _voteRepository = voteRepository;
            _voteCrudRepository = voteCrudRepository;
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

        public Result<PagedResult<VoteResponseDto>> GetBlogVotesByUser(int page, int pageSize, long userId)
        {
            var entities = _voteRepository.GetPagedByUserId(page, pageSize, userId);
            var items = entities.Results.Select(_mapper.Map<VoteResponseDto>).ToList();
            return new PagedResult<VoteResponseDto>(items, entities.TotalCount);
        }

        public Result SetVote(long blogId, long userId, API.Dtos.VoteType voteType)
        {
            var domainVoteType = (Domain.VoteType)voteType; // bruh...
            try
            {
                var vote = _voteRepository.GetByUserIdAndBlogId(userId, blogId);
                if (vote.VoteType == domainVoteType)
                {
                    _voteCrudRepository.Delete(vote.Id);
                    return Result.Ok();
                }

                vote.SetToVoteType(domainVoteType);
                _voteCrudRepository.Update(vote);
                return Result.Ok();
            }
            catch
            {
                try
                {
                    _voteCrudRepository.Create(new Vote(userId, blogId, domainVoteType));
                    return Result.Ok();
                }
                catch (ArgumentException e)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                }
            }
        }

    }
}
