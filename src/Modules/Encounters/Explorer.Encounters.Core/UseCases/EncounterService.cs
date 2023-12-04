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
        private readonly ITouristProgressRepository _touristProgressRepository;
        private readonly ICrudRepository<TouristProgress> _touristProgressCrudRepository;
        private readonly IInternalUserService _internalUserService;
        private readonly IMapper _mapper;
        public EncounterService(ICrudRepository<Encounter> repository, IEncounterRepository encounterRepository, ITouristProgressRepository touristProgressRepository, ICrudRepository<TouristProgress> touristProgressCrudRepository, IInternalUserService userService, IMapper mapper) : base(repository, mapper)
        {
            _encounterRepository = encounterRepository;
            _touristProgressRepository = touristProgressRepository;
            _touristProgressCrudRepository = touristProgressCrudRepository;
            _internalUserService = userService;
            _mapper = mapper;
        }

        public Result<HiddenLocationEncounterResponseDto> CreateHiddenLocationEncounter(HiddenLocationEncounterCreateDto encounter)
        {
            // problem sa konverzijom iz jednog enuma u drugi (iako su isti lol) ovo 0 na kraju
            var entity = CrudRepository.Create(new HiddenLocationEncounter(encounter.Picture, encounter.PictureLongitude, encounter.PictureLatitude, encounter.Title, encounter.Description, encounter.Longitude, encounter.Latitude, encounter.XpReward, 0));
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

    }
}
