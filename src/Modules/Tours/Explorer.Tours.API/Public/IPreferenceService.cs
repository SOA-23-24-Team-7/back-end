using FluentResults;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.API.Public
{
    public interface IPreferenceService
    {
        Result<PreferenceResponseDto> Create<TourPreferenceCreateDto>(TourPreferenceCreateDto tourPreferencesDto);
        Result<PreferenceResponseDto> GetByUserId(int id);
        Result Delete(long id);
        Result<PreferenceResponseDto> Update<TourPreferenceUpdateDto>(TourPreferenceUpdateDto tourPreferencesDto);
    }
}
