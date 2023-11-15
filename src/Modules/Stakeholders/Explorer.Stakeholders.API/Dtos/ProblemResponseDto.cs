namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemResponseDto
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public DateTime ReportedTime { get; set; }
        public long TouristId { get; set; }
        public UserResponseDto Tourist { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public long TourAuthorId { get; set; }
        public bool IsResolved { get; set; }
        public bool IsAnswered { get; set; }
        public DateTime Deadline { get; set; }
    }
}
