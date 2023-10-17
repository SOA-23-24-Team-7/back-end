using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogDto, Domain.Blog>, IBlogService
    {
        public BlogService(ICrudRepository<Domain.Blog> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {


        }
    }
}
