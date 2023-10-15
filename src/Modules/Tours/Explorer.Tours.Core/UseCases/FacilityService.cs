using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases;

public class FacilityService : CrudService<FacilityDto, Facility>, IFacilityService
{
    public FacilityService(ICrudRepository<Facility> repository, IMapper mapper) : base(repository, mapper) { }
}