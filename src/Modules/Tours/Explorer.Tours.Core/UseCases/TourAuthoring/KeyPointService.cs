using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.TourAuthoring;

public class KeyPointService : BaseService<KeyPoint>, IKeyPointService, IInternalKeyPointService
{
    private readonly IKeyPointRepository _keyPointRepository;
    private readonly ICampaignRepository _campaignRepository;

    public KeyPointService(IKeyPointRepository keyPointRepository, IMapper mapper, ICampaignRepository campaignRepository) : base(mapper)
    {
        _keyPointRepository = keyPointRepository;
        _campaignRepository = campaignRepository;
    }

    public Result<List<KeyPointResponseDto>> GetByTourId(long tourId)
    {
        try
        {
            var result = _keyPointRepository.GetByTourId(tourId);
            return MapToDto<KeyPointResponseDto>(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<KeyPointResponseDto> Create<KeyPointCreateDto>(KeyPointCreateDto keyPoint)
    {
        try
        {
            var result = _keyPointRepository.Create(MapToDomain(keyPoint));
            return MapToDto<KeyPointResponseDto>(result);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<KeyPointResponseDto> Update<KeyPointUpdateDto>(KeyPointUpdateDto keyPoint)
    {
        try
        {
            var result = _keyPointRepository.Update(MapToDomain(keyPoint));
            return MapToDto<KeyPointResponseDto>(result);
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

    public Result Delete(long id)
    {
        try
        {
            _keyPointRepository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<KeyPointResponseDto> GetFirstByTourId(long tourId)
    {
        try
        {
            var result = _keyPointRepository.GetFirstByTourId(tourId);
            return MapToDto<KeyPointResponseDto>(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<PagedResult<KeyPointResponseDto>> GetPaged(int page, int pageSize)
    {
        var allKeyPoints = _keyPointRepository.GetPaged(page, pageSize);
        var pagedResult = new PagedResult<KeyPoint>(allKeyPoints.Results, allKeyPoints.Results.Count);
        return MapToDto<KeyPointResponseDto>(pagedResult);
    }

    public bool IsToursAuthor(long userId, long id)
    {
        return _keyPointRepository.IsToursAuthor(userId, id);
    }

    public double GetKeyPointLongitude(long keyPointId)
    {
        return _keyPointRepository.GetKeyPointLongitude(keyPointId);
    }

    public double GetKeyPointLatitude(long keyPointId)
    {
        return _keyPointRepository.GetKeyPointLatitude(keyPointId);
    }

    public void AddEncounter(long keyPointId, bool isRequired)
    {
        var keyPoint = _keyPointRepository.Get(keyPointId);

        keyPoint.AddEncounter();
        if (isRequired) keyPoint.RequireEncounter();

        _keyPointRepository.Update(keyPoint);
    }

    public bool CheckEncounterExists(long keyPointId)
    {
        return _keyPointRepository.CheckEncounterExists(keyPointId);
    }

    public Result<List<KeyPointResponseDto>> GetByCampaignId(long campaignId)
    {
        Campaign campaign = _campaignRepository.GetById(campaignId);
        if (campaign == null)
            return null;
        campaign.KeyPoints.Clear();
        foreach (long keyPointId in campaign.KeyPointIds)
        {
            campaign.KeyPoints.Add(_keyPointRepository.Get(keyPointId));
        }
        List<KeyPoint>? keyPoints = new List<KeyPoint>(campaign.KeyPoints);
        return MapToDto<KeyPointResponseDto>(keyPoints);
    }

    public Result<KeyPointResponseDto> GetById(long id)
    {
        return MapToDto<KeyPointResponseDto>(_keyPointRepository.Get(id));
    }
}