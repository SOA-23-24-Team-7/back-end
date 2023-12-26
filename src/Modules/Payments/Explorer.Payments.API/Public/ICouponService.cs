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
        Result<PagedResult<CouponResponseDto>> GetPagedByAuthorId(int page, int pageSize,long id);
        Result<CouponResponseDto> Create<CouponCreateDto>(CouponCreateDto coupon);
        Result<CouponResponseDto> Update<CouponUpdateDto>(CouponUpdateDto coupon);
        Result Delete(long id);
    }
}
