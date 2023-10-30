using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public;

    public interface IPublicKeyPointRequestService
    {
        Result<PublicKeyPointRequestResponseDto> Create<PublicKeyPointRequestCreateDto>(PublicKeyPointRequestCreateDto request);
    }

