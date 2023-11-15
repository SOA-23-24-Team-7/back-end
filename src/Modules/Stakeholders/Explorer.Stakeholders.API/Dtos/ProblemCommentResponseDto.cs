namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemCommentResponseDto
    {
        public long CommenterId { get; set; }
        public UserResponseDto Commenter { get; set; }
        public string Text { get; set; }
    }
}
