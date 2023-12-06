using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public
{
    public interface IRecordService 
    {
        Result<RecordResponseDto> Create<RecordCreateDto>(RecordCreateDto record);
        Result<PagedResult<RecordResponseDto>> GetPagedByTouristId(int page, int pageSize, long touristId);

    }
}
