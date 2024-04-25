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
        public Task<string> GetFollowers(string encodedIds, int page, int pageSize);
        public Task<string> GetFollowings(string encodedIds, int page, int pageSize);
        Result Delete(long id);
        Result<FollowerResponseDto> Create<FollowerCreateDto>(FollowerCreateDto rating);
    }
}
