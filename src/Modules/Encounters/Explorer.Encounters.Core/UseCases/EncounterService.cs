using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Internal;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Encounter;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using FluentResults;
using System.Globalization;
using System.Xml.Linq;
using EncounterStatus = Explorer.Encounters.Core.Domain.Encounter.EncounterStatus;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterResponseDto, Encounter>, IEncounterService, IInternalEncounterService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IHiddenLocationEncounterRepository _hiddenLocationEncounterRepository;
        private readonly ITouristProgressRepository _touristProgressRepository;
        private readonly ICrudRepository<TouristProgress> _touristProgressCrudRepository;
        private readonly IInternalUserService _internalUserService;
        private readonly IInternalKeyPointService _keypointService;
        private readonly IKeyPointEncounterRepository _keypointEncounterRepository;
        private readonly IMapper _mapper;
        private readonly IMiscEncounterRepository _miscEncounterRepository;


        public EncounterService(ICrudRepository<Encounter> repository, IEncounterRepository encounterRepository, IHiddenLocationEncounterRepository hiddenLocationEncounterRepository, ITouristProgressRepository touristProgressRepository, ICrudRepository<TouristProgress> touristProgressCrudRepository, IInternalUserService userService, IMiscEncounterRepository miscEncounterRepository, IMapper mapper, IInternalKeyPointService keypointService, IKeyPointEncounterRepository keypointEncounterRepository) : base(repository, mapper)
        {
            _encounterRepository = encounterRepository;
            _hiddenLocationEncounterRepository = hiddenLocationEncounterRepository;
            _touristProgressRepository = touristProgressRepository;
            _touristProgressCrudRepository = touristProgressCrudRepository;
            _internalUserService = userService;
            _mapper = mapper;
            _keypointService = keypointService;
            _keypointEncounterRepository = keypointEncounterRepository;
            _miscEncounterRepository = miscEncounterRepository;
        }

        public Result<EncounterInstanceResponseDto> GetInstance(long userId, long encounterId)
        {
            var entity = _encounterRepository.GetInstance(userId, encounterId);
            return _mapper.Map<EncounterInstanceResponseDto>(entity);
        }

        public bool CheckIfUserInCompletionRange(long userId, long encounterId, double longitude, double latitude)
        {
            return _hiddenLocationEncounterRepository.GetHiddenLocationEncounterById(encounterId).isUserInCompletionRange(longitude, latitude);
        }

        public Result<HiddenLocationEncounterResponseDto> CreateHiddenLocationEncounter(HiddenLocationEncounterCreateDto encounter)
        {
            try
            {
                // problem sa konverzijom iz jednog enuma u drugi (iako su isti lol) ovo 0 na kraju
                var hiddenEncounter = new HiddenLocationEncounter(encounter.PictureLongitude, encounter.PictureLatitude, encounter.Title, encounter.Description, encounter.Picture, encounter.Longitude, encounter.Latitude, encounter.Radius, encounter.XpReward, 0, Domain.Encounter.EncounterType.Hidden);
                CrudRepository.Create(hiddenEncounter);
                return MapToDto<HiddenLocationEncounterResponseDto>(hiddenEncounter);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<PagedResult<EncounterResponseDto>> GetActive(int page, int pageSize)
        {
            var entities = _encounterRepository.GetActive(page, pageSize);
            return MapToDto<EncounterResponseDto>(entities);
        }

        public Result<HiddenLocationEncounterResponseDto> GetHiddenLocationEncounterById(long id)
        {
            try
            {
                var entity = _hiddenLocationEncounterRepository.GetHiddenLocationEncounterById(id);
                return MapToDto<HiddenLocationEncounterResponseDto>(entity);

            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
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
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<TouristProgressResponseDto> CompleteHiddenLocationEncounter(long userId, long encounterId, double longitute, double latitude)
        {
            var encounter = _hiddenLocationEncounterRepository.GetHiddenLocationEncounterById(encounterId);
            if (encounter.isUserInCompletionRange(longitute, latitude))
                return CompleteEncounter(userId, encounterId);
            return Result.Fail("User is not in 5m range");
        }

        public Result<PagedResult<EncounterResponseDto>> GetAllInRangeOf(double range, double longitude, double latitude, int page, int pageSize)
        {
            var entities = _encounterRepository.GetAllInRangeOf(range, longitude, latitude, page, pageSize);
            return MapToDto<EncounterResponseDto>(entities);
        }

        public Result<PagedResult<EncounterResponseDto>> GetAllDoneByUser(int currentUserId, int page, int pageSize)
        {
              var doneEncountersResult = _encounterRepository.GetAllDoneByUser(currentUserId, page, pageSize);

            return MapToDto<EncounterResponseDto>(doneEncountersResult);

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

                CrudRepository.Create(new KeyPointEncounter(keyPointEncounter.Title, keyPointEncounter.Description, keyPointEncounter.Picture, longitude, latitude, keyPointEncounter.Radius, keyPointEncounter.XpReward, EncounterStatus.Active, keyPointEncounter.KeyPointId));

                _keypointService.AddEncounter(keyPointEncounter.KeyPointId, keyPointEncounter.IsRequired);

                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.Forbidden).WithError(e.Message);
            }
        }

        public Result<KeyPointEncounterResponseDto> ActivateKeyPointEncounter(double longitude, double latitude,
            long keyPointId, long userId)
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
                var encounter = _keypointEncounterRepository.GetByKeyPoint(keyPointId);
                encounter.ActivateEncounter(userId, longitude, latitude);
                CrudRepository.Update(encounter);
                return MapToDto<KeyPointEncounterResponseDto>(encounter);
            }
            catch (Exception)
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
        }
        

        public Result<EncounterResponseDto> CancelEncounter(long userId, long encounterId)
        {
            try
            {
                var encounter = _encounterRepository.GetById(encounterId);
                encounter.CancelEncounter(userId);
                CrudRepository.Update(encounter);
                return MapToDto<EncounterResponseDto>(encounter);
            }
            catch (Exception)
            {

                return Result.Fail(FailureCode.InvalidArgument);
            }
        }

        public bool IsEncounterInstanceCompleted(long userId, long keyPointId)
        {
            return _keypointEncounterRepository.IsEncounterInstanceCompleted(userId, keyPointId);
        }

        public Result<MiscEncounterResponseDto> CreateMiscEncounter(MiscEncounterCreateDto encounter)
        {
            try
            {
                var miscEncounter = new MiscEncounter(encounter.ChallengeDone, encounter.Title, encounter.Description,encounter.Picture, encounter.Longitude, encounter.Latitude, encounter.Radius, encounter.XpReward, 0, Domain.Encounter.EncounterType.Misc);
                CrudRepository.Create(miscEncounter);
                return MapToDto<MiscEncounterResponseDto>(miscEncounter);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<SocialEncounterResponseDto> CreateSocialEncounter(SocialEncounterCreateDto encounterDto)
        {
            try
            {
                var encounter = new SocialEncounter(encounterDto.Title, encounterDto.Description, encounterDto.Picture, encounterDto.Longitude, encounterDto.Latitude, encounterDto.Radius, encounterDto.XpReward, (Domain.Encounter.EncounterStatus)encounterDto.Status, encounterDto.PeopleNumber, Domain.Encounter.EncounterType.Social);
                CrudRepository.Create(encounter);
                return MapToDto<SocialEncounterResponseDto>(encounter);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public KeyPointEncounterResponseDto GetByKeyPointId(long keyPointId)
        {
            return MapToDto<KeyPointEncounterResponseDto>(_keypointEncounterRepository.GetByKeyPointId(keyPointId));
        }





    }
}