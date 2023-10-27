namespace Explorer.Blog.API.Dtos
{
    public enum BlogStatus { Draft, Published, Closed };
    public class BlogResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<string>? Pictures { get; init; }
        public BlogStatus Status { get; init; }


    }
}
