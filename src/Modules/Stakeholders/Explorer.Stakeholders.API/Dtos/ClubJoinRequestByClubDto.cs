namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubJoinRequestByClubDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public string TouristName { get; set; }
        public DateTime RequestedAt { get; set; }
        public string Status { get; set; }
    }
}
