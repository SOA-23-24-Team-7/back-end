namespace Explorer.Blog.API.Dtos
{
    public enum VoteType { DOWNVOTE, UPVOTE }
    public class VoteResponseDto
    {
        public long UserId { get; set; }
        public VoteType VoteType { get; set; }
    }
}
