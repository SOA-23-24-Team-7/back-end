using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Blog.API.Dtos
{
    public enum BlogStatus { Draft, Published, Closed, Active, Famous };
    public enum BlogVisibilityPolicy { Public, Private };
    public class BlogResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<string>? Pictures { get; set; }
        public BlogStatus Status { get; set; }
        public List<CommentResponseDto> Comments { get; set; }
        public List<VoteResponseDto> Votes { get; set; }
        public BlogVisibilityPolicy VisibilityPolicy { get; set; }
        public long VoteCount { get; set; }
        public long UpvoteCount { get; set; }
        public long DownvoteCount { get; set; }
        public int AuthorId { get; set; }
        public UserResponseDto Author { get; set; }
        public long? ClubId { get; set; }
    }
}
