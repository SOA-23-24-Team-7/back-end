using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases
{
    public class PublicFacilityRequestService : CrudService<PublicFacilityRequestResponseDto, PublicFacilityRequest>, IPublicFacilityRequestService
    {
        private readonly ICrudRepository<PublicFacilityRequest> _repository;
        public PublicFacilityRequestService(ICrudRepository<PublicFacilityRequest> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }
    }
}
