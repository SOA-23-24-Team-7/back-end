using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Encounter;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterResponseDto, Encounter>, IEncounterService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IHiddenLocationEncounterRepository _hiddenLocationEncounterRepository;
        private readonly ITouristProgressRepository _touristProgressRepository;
        private readonly ICrudRepository<TouristProgress> _touristProgressCrudRepository;
        private readonly IInternalUserService _internalUserService;
        private readonly IMiscEncounterRepository _miscEncounterRepository;
        private readonly IMapper _mapper;

        public EncounterService(ICrudRepository<Encounter> repository, IEncounterRepository encounterRepository, IHiddenLocationEncounterRepository hiddenLocationEncounterRepository, ITouristProgressRepository touristProgressRepository, ICrudRepository<TouristProgress> touristProgressCrudRepository, IInternalUserService userService, IMiscEncounterRepository miscEncounterRepository, IMapper mapper) : base(repository, mapper)
        {
            _encounterRepository = encounterRepository;
            _hiddenLocationEncounterRepository = hiddenLocationEncounterRepository;
            _touristProgressRepository = touristProgressRepository;
            _touristProgressCrudRepository = touristProgressCrudRepository;
            _internalUserService = userService;
            _mapper = mapper;
            _miscEncounterRepository = miscEncounterRepository;
        }

        public Result<HiddenLocationEncounterResponseDto> CreateHiddenLocationEncounter(HiddenLocationEncounterCreateDto encounter)
        {
            // problem sa konverzijom iz jednog enuma u drugi (iako su isti lol) ovo 0 na kraju
            var entity = CrudRepository.Create(new HiddenLocationEncounter(encounter.Picture, encounter.PictureLongitude, encounter.PictureLatitude, encounter.Title, encounter.Description, encounter.Longitude, encounter.Latitude, encounter.Radius, encounter.XpReward, 0, Domain.Encounter.EncounterType.Hidden));
            return MapToDto<HiddenLocationEncounterResponseDto>(entity);
        }

        public Result<PagedResult<EncounterResponseDto>> GetActive(int page, int pageSize)
        {
            var entities = _encounterRepository.GetActive(page, pageSize);
            return MapToDto<EncounterResponseDto>(entities);
        }

        public Result<EncounterResponseDto> ActivateEncounter(long userId, long encounterId, double longitute, double latitude)
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
                encounter.ActivateEncounter(userId, longitute, latitude);
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
            var encounter = _hiddenLocationEncounterRepository.GetById(encounterId);
            if (encounter.isUserInCompletionRange(longitute, latitude))
                return CompleteEncounter(userId, encounterId);
            return Result.Fail("User is not in 5m range");
        }

        public Result<PagedResult<EncounterResponseDto>> GetAllInRangeOf(double range, double longitude, double latitude, int page, int pageSize)
        {
            var entities = _encounterRepository.GetAllInRangeOf(range, longitude, latitude, page, pageSize);
            return MapToDto<EncounterResponseDto>(entities);
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

        public Result CreateMiscEncounter(MiscEncounterCreateDto encounter)
        {
            
            var entity = CrudRepository.Create(new MiscEncounter(encounter.ChallengeDone, encounter.Title, encounter.Description, encounter.Longitude, encounter.Latitude,encounter.Radius, encounter.XpReward, 0, Domain.Encounter.EncounterType.Misc));
            return Result.Ok();
            //return MapToDto<MiscEncounterResponseDto>(entity);
        }



        public Result<SocialEncounterResponseDto> CreateSocialEncounter(SocialEncounterCreateDto encounterDto)
        {
            try
            {
                var encounter = new SocialEncounter(encounterDto.Title, encounterDto.Description, encounterDto.Longitude, encounterDto.Latitude, encounterDto.Radius, encounterDto.XpReward, (Domain.Encounter.EncounterStatus)encounterDto.Status, encounterDto.PeopleNumber, Domain.Encounter.EncounterType.Social);
                CrudRepository.Create(encounter);
                return MapToDto<SocialEncounterResponseDto>(encounter);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

    }
}