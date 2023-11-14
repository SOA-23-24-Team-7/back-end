using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class OrderItemService: CrudService<OrderItemResponseDto, OrderItem>, IOrderItemService
    {
        private readonly IMapper _mapper;
        
        public OrderItemService(ICrudRepository<OrderItem> repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
        }

       

    }
}
