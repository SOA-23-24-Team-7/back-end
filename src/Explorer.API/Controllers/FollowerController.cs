using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "nonAdministratorPolicy")]
    [Route("api/follower")]
    public class FollowerController : BaseApiController
    {
        private readonly IFollowerService _followerService;
        private readonly IUserService _userService;

        private readonly IHttpClientService _httpClientService;
        public FollowerController(IFollowerService followerService, IUserService userService, IHttpClientService httpClientService)
        {
            _followerService = followerService;
            _userService = userService;
            _httpClientService = httpClientService;
        }

        [HttpGet("followers/{id:long}")]
        public ActionResult<PagedResult<FollowerResponseWithUserDto>> GetFollowers([FromQuery] int page, [FromQuery] int pageSize, long id)
        {
            long userId = id;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _followerService.GetFollowers(page, pageSize, userId);
            return CreateResponse(result);
        }


        [HttpGet("followings/{id:long}")]
        public ActionResult<PagedResult<FollowingResponseWithUserDto>> GetFollowings([FromQuery] int page, [FromQuery] int pageSize, long id)
        {
            long userId = id;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _followerService.GetFollowings(page, pageSize, userId);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            var result = _followerService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<FollowerResponseDto> Create([FromBody] FollowerCreateDto follower)
        {
            var result = _followerService.Create(follower);
            return CreateResponse(result);
        }

        [HttpGet("search/{searchUsername}")]
        public ActionResult<PagedResult<UserResponseDto>> GetSearch([FromQuery] int page, [FromQuery] int pageSize, string searchUsername)
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _userService.SearchUsers(0, 0, searchUsername, userId);
            return CreateResponse(result);
        }

        [HttpPost("follow")]
        public async Task<ActionResult> Follow([FromBody] FollowerCreateDto follower)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8095, $"followers/follow/{follower.UserId}/{follower.FollowedById}");
           
            var response = await _httpClientService.PostAsync(uri,null);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return CreateResponse(Result.Ok(content));
            }
            else
            {
                return StatusCode(500, "Error following");
            }
        }



        [HttpPost("unfollow")]
        public async Task<ActionResult> Unfollow([FromBody] FollowerCreateDto follower)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8095, $"followers/unfollow/{follower.UserId}/{follower.FollowedById}");

            var response = await _httpClientService.PostAsync(uri, null);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return CreateResponse(Result.Ok(content));
            }
            else
            {
                return StatusCode(500, "Error following");
            }
        }

        [HttpGet("getFollowers/{id:long}")]
        public async Task<String> GetAllFollowers(long id)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8095, $"followers/getFollowers/{id}");
            var response = await _httpClientService.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("getFollowings/{id:long}")]
        public async Task<String> GetAllFollowings(long id)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8095, $"followers/getFollowings/{id}");
            var response = await _httpClientService.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            else
            {
                return null;
            }
        }
    }

}
