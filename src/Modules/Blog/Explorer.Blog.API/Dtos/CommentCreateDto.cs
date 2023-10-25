namespace Explorer.Blog.API.Dtos
{
    public class CommentCreateDto
    {
        public long AuthorId { get; set; }
        public long BlogId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
    }
}
