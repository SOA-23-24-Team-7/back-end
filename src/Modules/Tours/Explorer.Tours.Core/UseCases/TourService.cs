using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases;

public class TourService : CrudService<TourDto, Tour>, ITourService
{
    public TourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper) { }
}
