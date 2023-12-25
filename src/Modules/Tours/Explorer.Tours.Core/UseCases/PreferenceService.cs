using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using System;

namespace Explorer.Tours.Core.UseCases
{
    public class PreferenceService : CrudService<PreferenceResponseDto, Preference>, IPreferenceService
    {
        private readonly IPreferenceRepository _tourPreferenceRepository;
        private readonly IMapper _mapper;

        public PreferenceService(ICrudRepository<Preference> repository, IPreferenceRepository tourPreferenceRepository, IMapper mapper) : base(repository, mapper)
        {
            _tourPreferenceRepository = tourPreferenceRepository;
            _mapper = mapper;
        }

        public Result<PreferenceResponseDto> GetByUserId(int id)
        {
            try
            {
                var preference = _tourPreferenceRepository.GetByUserId(id);
                if (preference == null) throw new KeyNotFoundException("Not found!");
                var preferenceDto = _mapper.Map<PreferenceResponseDto>(preference);

                return Result.Ok(preferenceDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<PreferenceResponseDto>(ex.Message);
            }
        }
    }
}
