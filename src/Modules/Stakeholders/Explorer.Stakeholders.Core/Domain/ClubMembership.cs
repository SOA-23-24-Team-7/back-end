using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class ClubMembership : Entity
{
    public long ClubId { get; init; }
    public Club? Club { get; init; }
    public long TouristId { get; init; }
    public User? Tourist { get; init; }
    public DateTime TimeJoined { get; set; }

    public ClubMembership() : this(-1, -1) { }
    public ClubMembership(long clubId, long touristId) : this(clubId, touristId, DateTime.Now) { }
    public ClubMembership(long clubId, long touristId, DateTime timeJoined)
    {
        ClubId = clubId;
        TouristId = touristId;
        TimeJoined = timeJoined;
    }
}