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
        Result<RatingDto> Create(RatingDto rating);
        Result<PagedResult<RatingDto>> GetPaged(int page, int pageSize);
    }
}
