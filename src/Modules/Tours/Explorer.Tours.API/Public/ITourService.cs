using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public;

public interface ITourService
{
    Result<PagedResult<TourResponseDto>> GetPaged(int page, int pageSize);
    Result<TourResponseDto> Get(long id);
    Result<TourResponseDto> Create<TourCreateDto>(TourCreateDto tour);
    Result<TourResponseDto> Update<TourUpdateDto>(TourUpdateDto tour);
    Result DeleteCascade(long tourId);
    Result<PagedResult<TourResponseDto>> GetAuthorsPagedTours(long id, int page, int pageSize);
    Result<PagedResult<EquipmentResponseDto>> GetEquipment(long tourId);
    Result AddEquipment(long tourId, long equipmentId);
    Result DeleteEquipment(long tourId, int equipmentId);
    Result<TourResponseDto> GetById(long id); 
    Result Publish(long id, long authorId);
    Result<PagedResult<TourResponseDto>> GetPublished(int page, int pageSize);
    Result Archive(long id, long authorId);
    Result<PagedResult<TourResponseDto>> GetAllPaged(int page, int pageSize);
    Result<bool> CanTourBeRated(long tourId, long userId);
    Result<PagedResult<LimitedTourViewResponseDto>> GetPublishedLimitedView(int page, int pageSize);
    Result<PagedResult<LimitedTourViewResponseDto>> GetLimitedInfoTours(int page, int pageSize, List<long> ids);
    Result<List<TourResponseDto>> GetTours(List<long> toursIds);
    Result MarkAsReady(long id, long touristId);
    Result<PagedResult<TourResponseDto>> GetToursBasedOnSelectedKeyPoints(int page, int pageSize, List<long> publicKeyPointIds, long authorId);
    Result<PagedResult<TourResponseDto>> GetAdventureTours(int page, int pageSize);
    Result<PagedResult<TourResponseDto>> GetFamilyTours(int page, int pageSize);
    Result<PagedResult<TourResponseDto>> GetCruiseTours(int page, int pageSize);
    Result<PagedResult<TourResponseDto>> GetCulturalTours(int page, int pageSize);
}
