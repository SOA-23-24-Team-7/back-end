namespace Explorer.Stakeholders.API.Dtos
{
    public class RatingResponseDto
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string? Comment { get; set; }
        public DateTime DateTime { get; set; }
        public long UserId { get; set; }
    }
}
