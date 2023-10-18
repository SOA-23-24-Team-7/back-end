using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class ClubInvitation : Entity
{
    public long ClubId { get; init; }
    public long TouristId { get; init; }
    public DateTime TimeCreated { get; init; }
    public InvitationStatus Status { get; set; }

    public ClubInvitation() : this(-1, -1) { }
    public ClubInvitation(long clubId, long touristId) : this(-1, clubId, touristId) { }
    public ClubInvitation(long id, long clubId, long touristId) : this(id, clubId, touristId, DateTime.Now, InvitationStatus.Waiting) { }
    public ClubInvitation(long id, long clubId, long touristId, DateTime time, InvitationStatus status)
    {
        Id = id;
        ClubId = clubId;
        TouristId = touristId;
        TimeCreated = time;
        Status = status;
    }
}

public enum InvitationStatus
{
    Waiting,
    Accepted,
    Declined
}