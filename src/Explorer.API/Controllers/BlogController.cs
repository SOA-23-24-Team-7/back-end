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
using Grpc.Net.Client;
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
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogService authenticationService, IClubMemberManagementService clubMemberManagmentService, IClubService clubService, IHttpClientService httpClientService, ILogger<BlogController> logger)
        {
            _blogService = authenticationService;
            _clubMemberManagmentService = clubMemberManagmentService;
            _clubService = clubService;
            _httpClientService = httpClientService;
            _logger = logger;
        }


        [Authorize(Policy = "userPolicy")]
        [HttpPost("create")]
        public async Task<String> Create([FromBody] BlogCreationDto blog)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.CreateBlog(new BlogCreationRequest { 
                Title = blog.Title,
                Description = blog.Description,
                AuthorId = blog.AuthorId,
                BlogTopic = blog.BlogTopic
            });
            return reply.Message;
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
            using var channel = GrpcChannel.ForAddress("http://localhost:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.BlockBlog(new BlogIdRequest
            {
                Id = id
            });
            return reply.Message;
        }

        [Authorize(Policy = "userPolicy")]
        [HttpDelete("delete/{id:long}")]
        public async Task<String> Delete(int id)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.DeleteBlog(new BlogIdRequest
            {
                Id = id
            });
            return reply.Message;
        }

        [HttpGet]
        public async Task<List<BlogResponse>> GetAll()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.FindPublishedBlogs(new Empty{});
            List<BlogResponse> blogs = new List<BlogResponse>();
            foreach (var blog in reply.Blogs)
            {
                blogs.Add(blog);
            }
            return blogs;
        }

        [HttpGet("{id:long}")]
        public async Task<BlogResponse> Get(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.FindBlogById(new BlogIdRequest
            { 
                Id = id 
            });
            return reply;
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
        public async Task<List<BlogResponse>> GetWithType(string type)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.FindBlogsByType(new TypeRequest{
                Type = type
            });
            List<BlogResponse> blogs = new List<BlogResponse>();
            foreach (var blog in reply.Blogs)
            {
                blogs.Add(blog);
            }
            return blogs;
        }

        //NEW -RESTRICTING GET of all blogs- only followers can see blogs
        [Authorize(Policy = "userPolicy")]
        [HttpGet("following")]
        public async Task<List<BlogResponseSOADto>> GetFollowingBlogs()
        {
            //pronalazenje blogova 

            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "blogs/published");
            var response = await _httpClientService.GetAsync(uri);
            

            //fetching followings of the logged in user
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = long.Parse(identity.FindFirst("id").Value);
            string uriFollowing = _httpClientService.BuildUri(Protocol.HTTP, "follower-service", 8095, $"followers/getFollowing/{id}"); 
            var responseFollowing = await _httpClientService.GetAsync(uriFollowing);


            _logger.LogInformation($"Value of id: {id}");
            


            List<BlogResponseSOADto> returnValue = new List<BlogResponseSOADto>();
            if (response.IsSuccessStatusCode && responseFollowing.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var allblogs = System.Text.Json.JsonSerializer.Deserialize<List<BlogResponseSOADto>>(content);
                _logger.LogInformation($"Value of blogs: {allblogs?.ToString()}");

                var contentFollowing = await responseFollowing.Content.ReadAsStringAsync();
                var allFollowing = System.Text.Json.JsonSerializer.Deserialize<List<FollowerDto>>(contentFollowing);

                //filtering blogs
                foreach(BlogResponseSOADto blog in allblogs)
                {
                    if(allFollowing!= null &&( allFollowing.Find(f => f.UserId == blog.AuthorId) != null || blog.AuthorId == (int)id))
                    {
                        returnValue.Add(blog);
                    }
                    else if(blog.AuthorId == (int)id)
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

