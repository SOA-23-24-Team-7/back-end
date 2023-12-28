using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        Result<EncounterResponseDto> Create<EncounterCreateDto>(EncounterCreateDto encounter);
        Result<SocialEncounterResponseDto> CreateSocialEncounter(SocialEncounterCreateDto encounter);
        Result<HiddenLocationEncounterResponseDto> CreateHiddenLocationEncounter(HiddenLocationEncounterCreateDto encounter);
        Result<EncounterResponseDto> Update<EncounterUpdateDto>(EncounterUpdateDto encounter);
        Result<PagedResult<EncounterResponseDto>> GetPaged(int page, int pageSize);
        Result<PagedResult<EncounterResponseDto>> GetActive(int page, int pageSize);
        Result<EncounterResponseDto> Get(long id);
        Result<MiscEncounterResponseDto> CreateMiscEncounter(MiscEncounterCreateDto encounter);
        Result Delete(long id);
        Result<EncounterResponseDto> ActivateEncounter(long userId, long encounterId, double longitude, double latitude);
        Result<TouristProgressResponseDto> CompleteEncounter(long userId, long encounterId);
        Result CreateKeyPointEncounter(KeyPointEncounterCreateDto keyPointEncounter, long userId);
        Result<KeyPointEncounterResponseDto> ActivateKeyPointEncounter(double longitude, double latitude, long keyPointId, long userId);
        Result<TouristProgressResponseDto> CompleteHiddenLocationEncounter(long userId, long encounterId, double longitute, double latitude);
        Result<PagedResult<EncounterResponseDto>> GetAllInRangeOf(double range, double longitude, double latitude, int page, int pageSize); 
        Result<PagedResult<EncounterResponseDto>> GetAllDoneByUser(int currentUserId, int page, int pageSize);
        Result<EncounterResponseDto> CancelEncounter(long userId, long encounterId);
        Result<HiddenLocationEncounterResponseDto> GetHiddenLocationEncounterById(long id);
        Result<EncounterInstanceResponseDto> GetInstance(long userId, long encounterId);
        bool CheckIfUserInCompletionRange(long userId, long encounterId, double longitude, double latitude);
        KeyPointEncounterResponseDto GetByKeyPointId(long keyPointId);
        bool IsEncounterInstanceCompleted(long userId, long keyPointId);

    }
}