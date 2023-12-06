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
    public interface ICouponService
    {
        Result<PagedResult<CouponResponseDto>> GetPaged(int page, int pageSize);
        Result<CouponResponseDto> Create<CouponCreateDto>(CouponCreateDto review);
        Result<CouponResponseDto> Update<CouponUpdateDto>(CouponUpdateDto review);
        Result Delete(long id);
    }
}
