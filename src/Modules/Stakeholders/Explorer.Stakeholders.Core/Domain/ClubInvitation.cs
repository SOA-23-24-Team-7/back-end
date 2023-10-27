using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class ClubInvitation : Entity
{
    public long ClubId { get; init; }
    public Club? Club { get; init; }
    public long TouristId { get; init; }
    public DateTime TimeCreated { get; init; }
    public InvitationStatus Status { get; set; }

    public ClubInvitation() : this(-1, -1) { }
    public ClubInvitation(long clubId, long touristId) : this(clubId, touristId, DateTime.Now, InvitationStatus.Waiting) { }
    public ClubInvitation(long clubId, long touristId, DateTime time, InvitationStatus status)
    {
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