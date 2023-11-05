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
    public class PublicKeyPointRequestService : CrudService<PublicKeyPointRequestResponseDto, PublicKeyPointRequest>, IPublicKeyPointRequestService
    {
        private readonly ICrudRepository<PublicKeyPointRequest> _repository;
        public PublicKeyPointRequestService(ICrudRepository<PublicKeyPointRequest> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }
    }
}
