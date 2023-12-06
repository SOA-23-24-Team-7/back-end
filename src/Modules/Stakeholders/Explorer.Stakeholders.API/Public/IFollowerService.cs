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
    public interface IFollowerService
    {
        public Result<PagedResult<FollowerResponseWithUserDto>> GetFollowers(int page, int pageSize, long userId);
        public Result<PagedResult<FollowingResponseWithUserDto>> GetFollowings(int page, int pageSize, long userId);
        Result Delete(long id);
        Result<FollowerResponseDto> Create<FollowerCreateDto>(FollowerCreateDto rating);
    }
}
