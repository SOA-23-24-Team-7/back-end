using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;

namespace Explorer.Payments.Core.UseCases
{
    public class RecordService : CrudService<RecordResponseDto, Record>, IRecordService
    {
        private readonly IMapper _mapper;
        public RecordService(ICrudRepository<Record> repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
        }
    }
}
