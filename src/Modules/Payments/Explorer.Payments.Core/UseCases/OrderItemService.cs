using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class OrderItemService : CrudService<OrderItemResponseDto, OrderItem>, IOrderItemService
    {
        private readonly IMapper _mapper;

        public OrderItemService(ICrudRepository<OrderItem> repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
        }

    }
}
