namespace Explorer.Blog.API.Dtos
{
    public class BlogCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public BlogStatus Status { get; init; }
        public int AuthorId { get; set; }
        public long? ClubId { get; set; }
        public BlogVisibilityPolicy VisibilityPolicy { get; set; }
    }
}
