using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using System.Net;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TourPreferenceService : CrudService<TourPreferenceResponseDto,TourPreference>, ITourPreferenceService
    {
        private readonly ITourPreferenceRepository _tourPreferenceRepository;
        private readonly IMapper _mapper;

        public TourPreferenceService(ICrudRepository<TourPreference> repository, ITourPreferenceRepository tourPreferenceRepository, IMapper mapper) : base(repository, mapper)
        {
            _tourPreferenceRepository = tourPreferenceRepository;
            _mapper = mapper;
        }

        public Result<TourPreferenceResponseDto> GetByUserId(int id)
        {
            try
            {
                var preference = _tourPreferenceRepository.GetByUserId(id);
                var preferenceDto = _mapper.Map<TourPreferenceResponseDto>(preference);

                return Result.Ok(preferenceDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourPreferenceResponseDto>(ex.Message);
            }
        }
    }
}
