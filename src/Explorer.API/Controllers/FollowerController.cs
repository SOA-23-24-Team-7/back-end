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
            /*string uri = _httpClientService.BuildUri(Protocol.HTTP, "follower-service", 8095, $"followers/suggestions/{userId}");
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
            }*/

            using var channel = GrpcChannel.ForAddress("http://follower-service:8095");
            var client = new FollowerMicroservice.FollowerMicroserviceClient(channel);
            var reply = client.GetFollowerSuggestions(new FollowerIdRequest { Id = userId });
            List<FollowerResponse> followers = new List<FollowerResponse>();
            foreach (var follower in reply.Followers)
            {
                followers.Add(follower);
            }
            var users = followers.Select(u => u.Id).ToList();
            var followerDtos = users.Select(f => _userService.Get(f).Value).ToList();
            return Ok(followerDtos);
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

            using var channel = GrpcChannel.ForAddress("http://follower-service:8095");
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
            using var channel = GrpcChannel.ForAddress("http://follower-service:8095");
            var client = new FollowerMicroservice.FollowerMicroserviceClient(channel);
            var reply = client.FollowUser(new FollowRequest { UserID = follower.UserId, FollowerID = follower.FollowedById });
            return reply;
        }


        [HttpGet("getFollowers/{id:long}")]
        public async Task<List<FollowerResponse>> GetFollowers(int id)
        {
            using var channel = GrpcChannel.ForAddress("http://follower-service:8095");
            var client = new FollowerMicroservice.FollowerMicroserviceClient(channel);
            var reply = client.GetFollowers(new FollowerIdRequest { Id = id });
            List<FollowerResponse> followers = new List<FollowerResponse>();
            foreach (var follower in reply.Followers)
            {
                followers.Add(follower);
            }
            return followers;
        }
        [HttpGet("getFollowings/{id:long}")]
        public async Task<List<FollowerResponse>> GetFollowings(int id)
        {
            using var channel = GrpcChannel.ForAddress("http://follower-service:8095");
            var client = new FollowerMicroservice.FollowerMicroserviceClient(channel);
            var reply = client.GetFollowings(new FollowerIdRequest { Id = id });
            List<FollowerResponse> followers = new List<FollowerResponse>();
            foreach (var follower in reply.Followers)
            {
                followers.Add(follower);
            }
            return followers;
        }

    }

}
