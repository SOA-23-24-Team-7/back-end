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
    Result Delete(long id);
    Result<PagedResult<TourResponseDto>> GetAuthorsPagedTours(long id, int page, int pageSize);
    Result<PagedResult<EquipmentResponseDto>> GetEquipment(long tourId);
    Result AddEquipment(long tourId, long equipmentId);
    Result DeleteEquipment(long tourId, int equipmentId);
    Result<TourResponseDto> GetById(long id);   //dodato
    Result Publish(long id, long authorId);
    Result<PagedResult<TourResponseDto>> GetPublished(int page, int pageSize);
}
