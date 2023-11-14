namespace Explorer.Stakeholders.API.Dtos
{
    public class RatingCreateDto
    {
        public int Grade { get; set; }
        public string? Comment { get; set; }
        public DateTime DateTime { get; set; }
        public long UserId { get; set; }
    }
}
