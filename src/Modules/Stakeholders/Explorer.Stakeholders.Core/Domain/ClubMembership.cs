using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class ClubMembership : Entity
{
    public long InvitationId { get; init; }
    public DateTime TimeJoined { get; set; }
}