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
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class PublicKeyPointService : CrudService<PublicKeyPointResponseDto, PublicKeyPoint>, IPublicKeyPointService
    {
        public PublicKeyPointService(ICrudRepository<PublicKeyPoint> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
