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
    public interface IPublicFacilityRequestService
    {
        Result<PublicFacilityRequestResponseDto> Create<PublicFacilityRequestCreateDto>(PublicFacilityRequestCreateDto request);
        Result<PagedResult<PublicFacilityRequestResponseDto>> GetPaged(int page, int pageSize);
        Result<PublicFacilityRequestResponseDto> Update<PublicFacilityRequestUpdateDto>(PublicFacilityRequestUpdateDto request);
        Result Reject(long requestId, string comment, long adminId);
        Result Accept(long requestId, long adminId);
        Result<PagedResult<PublicFacilityRequestResponseDto>> GetPagedWithName(int page, int pageSize);
    }
}
