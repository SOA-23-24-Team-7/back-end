namespace Explorer.Tours.API.Dtos
{
    public class TourExecutionSessionResponseDto
    {
        public long Id { get; set; }
        public TourExecutionSessionStatus Status { get; set; }
        public long TourId { get; set; }
        public long TouristId { get; set; }
        public long NextKeyPointId { get; set; }
        public double Progress { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsCampaign { get; set; }
    }
    public enum TourExecutionSessionStatus
    {
        Started,
        Abandoned,
        Completed
    }
}
