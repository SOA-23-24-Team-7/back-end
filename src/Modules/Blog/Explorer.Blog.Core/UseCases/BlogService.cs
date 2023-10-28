using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogResponseDto, Domain.Blog>, IBlogService
    {
        private readonly IBlogRepository _repository;
        public BlogService(ICrudRepository<Domain.Blog> crudRepository, IBlogRepository repository, IMapper mapper) : base(crudRepository, mapper)
        {
            _repository = repository;
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
    }
}
