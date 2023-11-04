namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemCommentResponseDto
    {
        public long Id { get; set; }
        public UserResponseDto Commenter { get; set; }
        public long CommenterId { get; set; }
        public long ProblemAnswerId { get; set; }
        public string Text { get; set; }
    }
}
