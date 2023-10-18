namespace Explorer.Blog.API.Dtos
{
    public class CommentRequestDto
    {
        public long BlogId { get; set; }
        public string Text { get; set; }
    }
}
