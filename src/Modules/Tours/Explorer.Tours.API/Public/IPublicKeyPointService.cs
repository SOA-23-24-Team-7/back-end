using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public
{
    public interface IPublicKeyPointService
    {
        Result<PagedResult<PublicKeyPointResponseDto>> GetPaged(int page, int pageSize);  
        Result<PublicKeyPointResponseDto> Create<PublicKeyPointCreateDto>(PublicKeyPointCreateDto publicKeyPoint);
        Result<KeyPointResponseDto> CreatePrivateKeyPoint(int tourId, int publicKeyPointId);
    }
}
