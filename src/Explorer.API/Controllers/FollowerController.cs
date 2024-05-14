using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Explorer.API.Controllers
{
    [Route("api/follower")]
    public class FollowerController : BaseApiController
    {
        private readonly IFollowerService _followerService;
        private readonly IUserService _userService;
        private readonly IHttpClientService _httpClientService;
        private readonly IPersonService _personService;

        private readonly ILogger<FollowerController> _logger;
        public FollowerController(IFollowerService followerService, IUserService userService, IHttpClientService httpClientService, ILogger<FollowerController> logger, IPersonService personService)
        {
            _followerService = followerService;
            _userService = userService;
            _httpClientService = httpClientService;
            _logger = logger;
            _personService = personService;
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

        [HttpGet("suggestions/{userId:long}")]
        public async Task<ActionResult<User>> GetFollowerSuggestions(long userId)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "follower-service", 8095, $"followers/suggestions/{userId}");
            var response = await _httpClientService.GetAsync(uri);

            _logger.LogInformation($"GETTING FOLLOWER SUGGESTIONS");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserSOADto>>(content);

                var followers = users.Select(u => u.UserId).ToList();

                var followerDtos = followers.Select(f => _userService.Get(f).Value).ToList();
                _logger.LogInformation($"FOLLOWER SUGGESTIONS: {followerDtos?.ToString()}");

                return Ok(followerDtos);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
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
        public async Task<FollowerStringMessage> Follow([FromBody] FollowerCreateDto follower)
        {
            /*string uri = _httpClientService.BuildUri(Protocol.HTTP, "follower-service", 8095, $"followers/follow/{follower.UserId}/{follower.FollowedById}");

            var response = await _httpClientService.PostAsync(uri, null);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return CreateResponse(Result.Ok(content));
            }
            else
            {
                return StatusCode(500, "Error following");
            }*/

            using var channel = GrpcChannel.ForAddress("http://localhost:8095");
            var client = new FollowerMicroservice.FollowerMicroserviceClient(channel);
            var reply = client.FollowUser(new FollowRequest { UserID = follower.UserId, FollowerID=follower.FollowedById });
            return reply;
        }



        [HttpPost("unfollow")]
        public async Task<FollowerStringMessage> Unfollow([FromBody] FollowerCreateDto follower)
        {
           /* string uri = _httpClientService.BuildUri(Protocol.HTTP, "follower-service", 8095, $"followers/unfollow/{follower.UserId}/{follower.FollowedById}");

            var response = await _httpClientService.PostAsync(uri, null);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return CreateResponse(Result.Ok(content));
            }
            else
            {
                return StatusCode(500, "Error following");
            }*/
            using var channel = GrpcChannel.ForAddress("http://localhost:8095");
            var client = new FollowerMicroservice.FollowerMicroserviceClient(channel);
            var reply = client.FollowUser(new FollowRequest { UserID = follower.UserId, FollowerID = follower.FollowedById });
            return reply;
        }


        [HttpGet("getFollowers/{id:long}")]
        public async Task<FollowerListResponse> GetFollowers(int id)
        {
            /*string uri = _httpClientService.BuildUri(Protocol.HTTP, "follower-service", 8095, $"followers/getFollowers/{id}");
            var response = await _httpClientService.GetAsync(uri);

            _logger.LogInformation($"GETTING FOLLOWERS");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserSOADto>>(content);

                // Extract user IDs into a list
                var followers = users.Select(u => u.UserId).ToList();

                // Convert follower IDs to appropriate DTOs
                var followerDtos = followers.Select(f => new FollowerResponseWithUserDto
                {
                    FollowedBy = _userService.Get(f).Value,
                    FollowedByPerson = _personService.Get(f).Value,

                }).ToList();
                _logger.LogInformation($"FOLLOWERS: {followerDtos?.ToString()}");

                return Ok(followerDtos);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }*/
            using var channel = GrpcChannel.ForAddress("http://localhost:8095");
            var client = new FollowerMicroservice.FollowerMicroserviceClient(channel);
            var reply = client.GetFollowers(new FollowerIdRequest { Id = id });
            return reply;
        }
        [HttpGet("getFollowings/{id:long}")]
        public async Task<FollowerListResponse> GetFollowings(int id)
        {
            /*string uri = _httpClientService.BuildUri(Protocol.HTTP, "follower-service", 8095, $"followers/getFollowing/{id}");
            var response = await _httpClientService.GetAsync(uri);

            _logger.LogInformation($"GETTING FOLLOWINGS");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserSOADto>>(content);

                // Extract user IDs into a list
                var followers = users.Select(u => u.UserId).ToList();

                // Convert follower IDs to appropriate DTOs
                var followerDtos = followers.Select(f => new FollowingResponseWithUserDto
                {
                    Following = _userService.Get(f).Value,
                    FollowingPerson = _personService.Get(f).Value,

                }).ToList();
                _logger.LogInformation($"FOLLOWINGS: {followerDtos?.ToString()}");

                return Ok(followerDtos);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }*/
            using var channel = GrpcChannel.ForAddress("http://localhost:8095");
            var client = new FollowerMicroservice.FollowerMicroserviceClient(channel);
            var reply = client.GetFollowings(new FollowerIdRequest { Id = id });
            return reply;
        }

    }

}
