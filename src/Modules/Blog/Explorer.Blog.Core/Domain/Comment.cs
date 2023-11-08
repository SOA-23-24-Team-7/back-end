using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public class Comment : Entity
    {
        public long AuthorId { get; init; }
        public long BlogId { get; init; }
        public Blog? Blog { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; private set; }
        public string Text { get; private set; }

        public Comment(long authorId, long blogId, DateTime createdAt, DateTime? updatedAt, string text)
        {
            Validate(text);
            AuthorId = authorId;
            BlogId = blogId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Text = text;
        }

        public void UpdateText(string text)
        {
            Validate(text);
            Text = text;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Validate(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Invalid comment text.");
        }
    }
}
