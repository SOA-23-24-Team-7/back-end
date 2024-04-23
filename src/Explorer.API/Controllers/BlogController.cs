using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Explorer.API.Controllers
{

    [Route("api/blog")]
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;
        private readonly IClubMemberManagementService _clubMemberManagmentService;
        private readonly IClubService _clubService;
        private readonly IHttpClientService _httpClientService;

        public BlogController(IBlogService authenticationService, IClubMemberManagementService clubMemberManagmentService, IClubService clubService, IHttpClientService httpClientService)
        {
            _blogService = authenticationService;
            _clubMemberManagmentService = clubMemberManagmentService;
            _clubService = clubService;
            _httpClientService = httpClientService;
        }


        [Authorize(Policy = "userPolicy")]
        [HttpPost("create")]
        public async Task<String> Create([FromBody] BlogCreationDto blog)
        {
            blog.AuthorId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "blogs");
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(blog);
            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClientService.PostAsync(uri, requestContent);
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

        [Authorize(Policy = "userPolicy")]
        [HttpPut("update")]
        public ActionResult<BlogResponseDto> Update([FromBody] BlogUpdateDto blog)
        {
            blog.AuthorId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _blogService.Update(blog);
            return CreateResponse(result);
        }

        [HttpPatch("block/{id:long}")]
        public async Task<String> Block(long id)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, $"blogs/{id}");
            var response = await _httpClientService.PatchAsync(uri, null);
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

        [Authorize(Policy = "userPolicy")]
        [HttpDelete("delete/{id:long}")]
        public async Task<String> Delete(int id)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, $"blogs/{id}");
            var response = await _httpClientService.DeleteAsync(uri);
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

        [HttpGet]
        public async Task<String> GetAll()
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "blogs/published");
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

        [HttpGet("{id:long}")]
        public async Task<String> Get(long id)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, $"blogs/{id}");
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

        [HttpPut("{id:int}")]
        public ActionResult<BlogResponseDto> Update([FromBody] BlogUpdateDto blog, int id)
        {
            blog.Id = id;
            var result = _blogService.UpdateBlog(blog);
            return CreateResponse(result);
        }




        [Authorize(Policy = "userPolicy")]
        [HttpGet("upvotsahde/{id:long}")]
        public ActionResult Upvote(long id)
        {
            if (_blogService.IsBlogClosed(id)) return CreateResponse(Result.Fail(FailureCode.InvalidArgument));

            var userId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _blogService.SetVote(id, userId, Blog.API.Dtos.VoteType.UPVOTE);
            return CreateResponse(result);
        }

        //[Authorize(Policy = "userPolicy")]

        [HttpGet("upvote/{id:long}")]
        public async Task<ActionResult> UpvoteBlog(long id)
        {

            var userId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);

            var voteRequest = new VoteCreateDto
            {
                UserId = userId,
                BlogId = id,
                VoteType = "UPVOTE"
            };

            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "blogs/votes");

            try
            {
                var json = JsonConvert.SerializeObject(voteRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClientService.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var goCommentResponse = JsonConvert.DeserializeObject<CommentResponseDto>(responseString);

                    return CreateResponse(Result.Ok(goCommentResponse));
                }
                else
                {
                    return StatusCode(500, "Error voting");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error voting");
            }

        }

        [Authorize(Policy = "userPolicy")]
        [HttpGet("downvosdbcte/{id:long}")]
        public ActionResult Downvote(long id)
        {
            if (_blogService.IsBlogClosed(id)) return CreateResponse(Result.Fail(FailureCode.InvalidArgument));

            var userId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _blogService.SetVote(id, userId, Blog.API.Dtos.VoteType.DOWNVOTE);
            return CreateResponse(result);
        }

        [HttpGet("downvote/{id:long}")]
        public async Task<ActionResult> DownvoteBlog(long id)
        {

            var userId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);

            var voteRequest = new VoteCreateDto
            {
                UserId = userId,
                BlogId = id,
                VoteType = "DOWNVOTE"
            };

            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "blogs/votes");

            try
            {
                var json = JsonConvert.SerializeObject(voteRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClientService.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var goCommentResponse = JsonConvert.DeserializeObject<CommentResponseDto>(responseString);

                    return CreateResponse(Result.Ok(goCommentResponse));
                }
                else
                {
                    return StatusCode(500, "Error voting");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error voting");
            }

        }


        [Authorize(Policy = "touristPolicy")]
        [HttpPost("createClubBlog")]
        public ActionResult<BlogResponseDto> CreateClubBlog([FromBody] BlogCreateDto blog)
        {
            blog.AuthorId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            if(blog.ClubId == null)
            {
                return BadRequest();
            }
            bool touristInClub = false;
            foreach(int touristId in _clubMemberManagmentService.GetMembers((long)blog.ClubId).Value.Results.Select(member => member.UserId))
            {
                if(touristId == blog.AuthorId)
                {
                    touristInClub = true;
                    break;
                }
            }
            if (!touristInClub)
            {
                if(_clubService.GetById((int)blog.ClubId).Value.OwnerId != blog.AuthorId)
                {
                    return BadRequest();
                }
            }
            blog.Date = DateTime.UtcNow;
            var result = _blogService.Create(blog);
            return CreateResponse(result);
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpGet("getClubBlogs")]
        public ActionResult<BlogResponseDto> GetClubBlogs([FromQuery] int page, [FromQuery] int pageSize, long clubId)
        {
            var result = _blogService.GetAllFromBlogs(page, pageSize, clubId);
            return CreateResponse(result);
        }

        [HttpGet("type/{type}")]
        public async Task<String> GetWithType(string type)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "blogs/type/" + type);


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

        //NEW -RESTRICTING GET of all blogs- only followers can see blogs
        [Authorize(Policy = "userPolicy")]
        [HttpGet("following")]
        public async Task<List<BlogResponseDto>> GetFollowingBlogs()
        {
            //pronalazenje blogova 

            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "blogs/published");
            var response = await _httpClientService.GetAsync(uri);
            

            //fetching followings of the logged in user
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = long.Parse(identity.FindFirst("id").Value);
            string uriFollowing = _httpClientService.BuildUri(Protocol.HTTP, "follower-service", 8095, $"followers/getFollowing/{id}"); ; //IZMIJENITI KAD SE DOKERIZUJE
            var responseFollowing = await _httpClientService.GetAsync(uriFollowing);

            List<BlogResponseDto> returnValue = new List<BlogResponseDto>();
            if (response.IsSuccessStatusCode && responseFollowing.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var allblogs = System.Text.Json.JsonSerializer.Deserialize<List<BlogResponseDto>>(content);

                var contentFollowing = await responseFollowing.Content.ReadAsStringAsync();
                var allFollowing = System.Text.Json.JsonSerializer.Deserialize<List<FollowerDto>>(contentFollowing);

                //filtering blogs
                foreach(BlogResponseDto blog in allblogs)
                {
                    if(allFollowing.Find(f => f.UserId == blog.AuthorId) != null)
                    {
                        returnValue.Add(blog);
                    }
                }

                return returnValue;
            }
            else
            {
                return null;
            }
        }


    }

}

