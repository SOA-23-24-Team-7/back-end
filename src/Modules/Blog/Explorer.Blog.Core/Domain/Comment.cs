using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public class Comment : Entity
    {
        public long AuthorId { get; init; }
        public long BlogId { get; init; }
        //public Blog Blog { get; private set; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public string Text { get; init; }

        public Comment() { }
        public Comment(long authorId, long blogId, DateTime createdAt, DateTime? updatedAt, string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Invalid comment text.");
            AuthorId = authorId;
            BlogId = blogId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Text = text;
        }
    }
}
