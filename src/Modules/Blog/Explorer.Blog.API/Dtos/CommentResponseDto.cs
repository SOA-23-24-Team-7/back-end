using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Blog.API.Dtos
{
    public class CommentResponseDto
    {
        public long Id { get; set; }
        public long AuthorId { get; set; }
        public long BlogId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Text { get; set; }
        public UserResponseDto Author { get; set; }
    }
}
