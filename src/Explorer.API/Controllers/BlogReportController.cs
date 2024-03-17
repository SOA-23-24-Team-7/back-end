using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Explorer.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/reports")]
    public class BlogReportController : BaseApiController
    {
        private readonly IHttpClientService _httpClientService;
        public BlogReportController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        [HttpGet("{id:long}")]
        public async Task<String> GetByBlog(long id)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8090, $"reports/{id}");
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

        [HttpPost]
        public async Task<String> Create([FromBody] BlogReportCreateDto report)
        {
            report.UserId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8090, "reports");
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(report);
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
    }
}
