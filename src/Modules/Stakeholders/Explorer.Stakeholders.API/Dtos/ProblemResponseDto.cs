namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemResponseDto
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public long TouristId { get; set; }
        public string TouristName { get; set; }
        public string TouristSurname { get; set; }
        public string TouristProfilePicture { get; set; }
        public string TouristUsername { get; set; }
        public int TourId { get; set; }
        public bool IsResolved { get; set; }
    }
}
