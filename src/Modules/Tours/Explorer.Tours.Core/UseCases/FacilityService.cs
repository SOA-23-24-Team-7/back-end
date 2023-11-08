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
    private readonly ICrudRepository<PublicFacilityRequest> _requestRepository;
    public FacilityService(ICrudRepository<Facility> repository, IMapper mapper, ICrudRepository<PublicFacilityRequest> requestRepository) : base(repository, mapper)
    {
        _repository = repository;
        _requestRepository = requestRepository;
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

    public Result<PagedResult<FacilityResponseDto>> GetPublic()
    {
        try
        {
            var acceptedRequests = _requestRepository.GetPaged(0,0).Results.ToList().FindAll(req => req.Status == PublicStatus.Accepted);
            var result = _repository.GetPaged(0,0).Results.ToList().FindAll(fac => (acceptedRequests.Find(req => req.FacilityId == fac.Id) != null));
            return MapToDto<FacilityResponseDto>(new PagedResult<Facility>(result,result.Count));
        }
        catch
        {
            return Result.Fail(FailureCode.Internal);
        }
    }
}