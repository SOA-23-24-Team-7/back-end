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
using Explorer.BuildingBlocks.Core.UseCases;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using System.Net;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TourPreferencesService : ITourPreferencesService
    {
        private readonly ITourPreferenceRepository _tourPreferenceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TourPreferencesService(ITourPreferenceRepository tourPreferenceRepository, IUserRepository userRepository, IMapper mapper)
        {
            _tourPreferenceRepository = tourPreferenceRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Result<TourPreferencesDto> Create(TourPreferencesDto tourPreferencesDto)
        {
            try
            {
                var tourPreference = _mapper.Map<TourPreference>(tourPreferencesDto);

                var createdPreference = _tourPreferenceRepository.Create(tourPreference);

                var createdDto = _mapper.Map<TourPreferencesDto>(createdPreference);

                return Result.Ok(createdDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourPreferencesDto>(ex.Message);
            }
        }

        public Result<TourPreferencesDto> GetByUserId(int id)
        {
            try
            {
                var preference = _tourPreferenceRepository.GetByUserId(id);
                var preferenceDto = _mapper.Map<TourPreferencesDto>(preference);

                return Result.Ok(preferenceDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourPreferencesDto>(ex.Message);
            }
        }
    }
}
