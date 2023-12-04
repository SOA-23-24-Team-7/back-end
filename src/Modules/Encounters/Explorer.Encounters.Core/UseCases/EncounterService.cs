using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Internal;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Encounter;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Internal;
using FluentResults;
using EncounterStatus = Explorer.Encounters.Core.Domain.Encounter.EncounterStatus;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterResponseDto, Encounter>, IEncounterService, IInternalEncounterService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly ITouristProgressRepository _touristProgressRepository;
        private readonly ICrudRepository<TouristProgress> _touristProgressCrudRepository;
        private readonly IInternalUserService _internalUserService;
        private readonly IInternalKeyPointService _keypointService;
        private readonly IKeyPointEncounterRepository _keypointEncounterRepository;
        private readonly IMapper _mapper;
        public EncounterService(ICrudRepository<Encounter> repository, IEncounterRepository encounterRepository, ITouristProgressRepository touristProgressRepository, ICrudRepository<TouristProgress> touristProgressCrudRepository, IInternalUserService userService, IMapper mapper, IInternalKeyPointService keypointService, IKeyPointEncounterRepository keypointEncounterRepository) : base(repository, mapper)
        {
            _encounterRepository = encounterRepository;
            _touristProgressRepository = touristProgressRepository;
            _touristProgressCrudRepository = touristProgressCrudRepository;
            _internalUserService = userService;
            _mapper = mapper;
            _keypointService = keypointService;
            _keypointEncounterRepository = keypointEncounterRepository;
        }

        public Result<PagedResult<EncounterResponseDto>> GetActive(int page, int pageSize)
        {
            var entities = _encounterRepository.GetActive(page, pageSize);
            return MapToDto<EncounterResponseDto>(entities);
        }

        public Result<EncounterResponseDto> ActivateEncounter(long userId, long encounterId, double longitude, double latitude)
        {
            try
            {
                _touristProgressRepository.GetByUserId(userId);
            }
            catch (Exception)
            {
                _touristProgressCrudRepository.Create(new TouristProgress(userId, 0, 1));
            }

            try
            {
                var encounter = _encounterRepository.GetById(encounterId);
                encounter.ActivateEncounter(userId, longitude, latitude);
                CrudRepository.Update(encounter);
                return MapToDto<EncounterResponseDto>(encounter);
            }
            catch (Exception)
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
        }

        public Result<TouristProgressResponseDto> CompleteEncounter(long userId, long encounterId)
        {
            try
            {
                var encounter = _encounterRepository.GetById(encounterId);
                encounter.CompleteEncounter(userId);
                var touristProgress = _touristProgressRepository.GetByUserId(userId);
                touristProgress.AddXp(encounter.XpReward);
                var responseDto = _mapper.Map<TouristProgressResponseDto>(touristProgress);
                responseDto.User = _internalUserService.Get(userId).Value;

                CrudRepository.Update(encounter);
                _touristProgressCrudRepository.Update(touristProgress);

                return responseDto;
            }
            catch (Exception)
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
        }

        public Result CreateKeyPointEncounter(KeyPointEncounterCreateDto keyPointEncounter, long userId)
        {
            try
            {
                if (!_keypointService.IsToursAuthor(userId, keyPointEncounter.KeyPointId)) return Result.Fail(FailureCode.Forbidden).WithError("Unauthorized");
                if (_keypointService.CheckEncounterExists(keyPointEncounter.KeyPointId)) return Result.Fail(FailureCode.Forbidden).WithError("Encounter already exists on this key point");

                var longitude = _keypointService.GetKeyPointLongitude(keyPointEncounter.KeyPointId);
                var latitude = _keypointService.GetKeyPointLatitude(keyPointEncounter.KeyPointId);

                CrudRepository.Create(new KeyPointEncounter(keyPointEncounter.Title, keyPointEncounter.Description, longitude, latitude, keyPointEncounter.XpReward, EncounterStatus.Active, keyPointEncounter.KeyPointId));

                _keypointService.AddEncounter(keyPointEncounter.KeyPointId, keyPointEncounter.IsRequired);

                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.Forbidden).WithError(e.Message);
            }
        }

        public bool IsEncounterInstanceCompleted(long userId, long keyPointId)
        {
            return _keypointEncounterRepository.IsEncounterInstanceCompleted(userId, keyPointId);
        }
    }
}
