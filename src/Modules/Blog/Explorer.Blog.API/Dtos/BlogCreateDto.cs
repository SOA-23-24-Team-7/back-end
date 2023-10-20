namespace Explorer.Blog.API.Dtos
{
    public class BlogCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<string>? Pictures { get; init; }
        public BlogStatus Status { get; init; }


    }
}
