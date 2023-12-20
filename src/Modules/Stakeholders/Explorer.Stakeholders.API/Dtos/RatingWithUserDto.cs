namespace Explorer.Stakeholders.API.Dtos
{
    public class RatingWithUserDto
    {
        public long Id { get; set; }
        public int Grade { get; set; }
        public string? Comment { get; set; }
        public DateTime DateTime { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
