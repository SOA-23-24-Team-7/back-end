using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class SubscriberService : CrudService<SubscriberResponseDto, Subscriber>, ISubscriberService
    {
        public SubscriberService(ICrudRepository<Subscriber> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
