namespace Explorer.Tours.API.Dtos
{
    public class ReviewUpdateDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public long TouristId { get; set; }
        public DateOnly TourVisitDate { get; set; }
        public DateOnly CommentDate { get; set; }
        public long TourId { get; set; }
        public List<string> Images { get; set; }
    }
}
