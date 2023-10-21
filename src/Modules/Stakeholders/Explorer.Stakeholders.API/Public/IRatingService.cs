using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IRatingService
    {
        Result<RatingResponseDto> Create<RatingCreateDto>(RatingCreateDto rating);
        Result<RatingResponseDto> Update<RatingUpdateDto>(RatingUpdateDto rating);
        Result Delete(long id);
        Result<PagedResult<RatingResponseDto>> GetPaged(int page, int pageSize);
        Result<PagedResult<RatingWithUserDto>> GetRatingsPaged(int page, int pageSize);
        Result<RatingResponseDto> GetByUser(long id);
    }
}
