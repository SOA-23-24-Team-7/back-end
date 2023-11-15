namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemUpdateDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public DateTime ReportedTime { get; set; }
        public long TouristId { get; set; }
        public int TourId { get; set; }
    }
}
