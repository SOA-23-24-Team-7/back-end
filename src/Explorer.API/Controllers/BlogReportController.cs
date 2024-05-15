using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Grpc.Net.Client;
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
        public async Task<List<ReportResponse>> GetByBlog(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://blog-service:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.FindReportsByBlog(new BlogIdRequest 
            {
                Id = id
            });
            List<ReportResponse> reports = new List<ReportResponse>();
            foreach (var report in reply.Reports)
            {
                reports.Add(report);
            }
            return reports;
        }

        [HttpPost]
        public async Task<String> Create([FromBody] BlogReportCreateDto report)
        {
            using var channel = GrpcChannel.ForAddress("http://blog-service:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.CreateReport(new ReportRequest
            {
                BlogId = report.BlogId,
                UserId = report.UserId,
                Reason = report.Reason,
            });
            return reply.Message;
        }
    }
}
