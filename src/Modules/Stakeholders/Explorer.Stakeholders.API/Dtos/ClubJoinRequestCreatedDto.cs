namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubJoinRequestCreatedDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public long ClubId { get; set; }
        public DateTime RequestedAt { get; set; }
        public string Status { get; set; }
    }
}
