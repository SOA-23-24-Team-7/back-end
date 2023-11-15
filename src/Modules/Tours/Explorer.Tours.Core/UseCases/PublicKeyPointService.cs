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
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class PublicKeyPointService : CrudService<PublicKeyPointResponseDto, PublicKeyPoint>, IPublicKeyPointService
    {
        private readonly ICrudRepository<PublicKeyPoint> _repository;
        private readonly IKeyPointRepository _keyPointRepository;
        private readonly IMapper _mapper;
        public PublicKeyPointService(ICrudRepository<PublicKeyPoint> repository, IMapper mapper,IKeyPointRepository keyPointRepository) : base(repository, mapper) 
        {
            _repository = repository;
            _keyPointRepository = keyPointRepository;
            _mapper = mapper;
        }

        public Result<KeyPointResponseDto> CreatePrivateKeyPoint(int tourId, int publicKeyPointId)
        {
            try
            {
                PublicKeyPoint publicKP = _repository.Get(publicKeyPointId);
                if(_keyPointRepository.GetByTourId(tourId).Find(kp => kp.Longitude == publicKP.Longitude && kp.Latitude == publicKP.Latitude) != null) 
                {
                    return Result.Fail(FailureCode.Forbidden);
                }
                KeyPoint keypoint = new KeyPoint(tourId, publicKP);
                var result = _keyPointRepository.Create(keypoint);
                return _mapper.Map<KeyPointResponseDto>(result);
            }
            catch(Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        
    }
}
