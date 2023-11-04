using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogResponseDto, Domain.Blog>, IBlogService
    {
        
        public BlogService(ICrudRepository<Domain.Blog> repository, IMapper mapper) : base(repository, mapper)
        {
            
        }

        public Result<BlogResponseDto> UpdateBlog(BlogUpdateDto blogUpdateDto)
        {
            try
            {
                var blog = CrudRepository.Get(blogUpdateDto.Id);
                blog.UpdateBlog(blogUpdateDto.Title, blogUpdateDto.Description, blogUpdateDto.Pictures,(Domain.BlogStatus)blogUpdateDto.Status);
                CrudRepository.Update(blog);

                return MapToDto<BlogResponseDto>(blog);

            }
            catch 
            {

                return Result.Fail(FailureCode.NotFound);
            }
        }
        
    }
}
