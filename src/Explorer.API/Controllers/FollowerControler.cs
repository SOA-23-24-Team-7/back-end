using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "nonAdministratorPolicy")]
    [Route("api/follower")]
    public class FollowerControler : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IFollowerService _followerService;
        public FollowerControler(IUserService userService, IFollowerService followerService)
        {
            _userService = userService;
            _followerService = followerService;
        }

        [HttpGet("followers")]
        public ActionResult<PagedResult<FollowerDto>> GetFollowers([FromQuery] int page, [FromQuery] int pageSize)
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _followerService.GetFollowers(page, pageSize, userId);
            return CreateResponse(result);
        }
    }

}
