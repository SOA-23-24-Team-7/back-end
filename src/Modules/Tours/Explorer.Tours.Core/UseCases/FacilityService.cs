using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases;

public class FacilityService : CrudService<FacilityResponseDto, Facility>, IFacilityService
{
    private readonly ICrudRepository<Facility> _repository;
    public FacilityService(ICrudRepository<Facility> repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public Result<PagedResult<FacilityResponseDto>> GetPagedByAuthorId(int page, int pageSize, int authorId)
    {
        try
        {
            var result = _repository.GetPaged(page, pageSize).Results.Where(f => f.AuthorId == authorId).ToList();
            var pagedResult = new PagedResult<Facility>(result, result.Count);

            return MapToDto<FacilityResponseDto>(pagedResult);

        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}