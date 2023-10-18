using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class ClubMembership : Entity
{
    public long ClubId { get; init; }
    public long TouristId { get; init; }
    public DateTime TimeJoined { get; set; }

    public ClubMembership() : this(-1, -1) { }
    public ClubMembership(long clubId, long touristId) : this(-1, clubId, touristId, DateTime.Now) { }
    public ClubMembership(long id, long clubId, long touristId) : this(id, clubId, touristId, DateTime.Now) { }
    public ClubMembership(long id, long clubId, long touristId, DateTime timeJoined)
    {
        Id = id;
        ClubId = clubId;
        TouristId = touristId;
        TimeJoined = timeJoined;
    }
}