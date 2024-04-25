using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    
    public class BlogResponseSOADto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("pictures")]
        public List<string>? Pictures { get; set; }
        [JsonPropertyName("status")]
        public String Status { get; set; }
        [JsonPropertyName("comments")]
        public List<String> Comments { get; set; }
        [JsonPropertyName("votes")]
        public List<VoteResponseDto> Votes { get; set; }
        [JsonPropertyName("visibilityPolicy")]
        public String VisibilityPolicy { get; set; }
        [JsonPropertyName("voteCount")]
        public long VoteCount { get; set; }
        [JsonPropertyName("upvoteCount")]
        public long UpvoteCount { get; set; }
        [JsonPropertyName("downvoteCOunt")]
        public long DownvoteCount { get; set; }
        [JsonPropertyName("authorId")]
        public int AuthorId { get; set; }
        [JsonPropertyName("author")]
        public UserResponseDto Author { get; set; }
        [JsonPropertyName("clubId")]
        public long? ClubId { get; set; }
        [JsonPropertyName("blogTopic")]
        public string BlogTopic { get; set; }
        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }
    }
}
