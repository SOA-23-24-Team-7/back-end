using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubJoinRequest : Entity
    {
        public long TouristId { get; init; }
        public User? Tourist { get; init; }
        public long ClubId { get; init; }
        public Club? Club { get; init; }
        public DateTime RequestedAt { get; init; }
        public ClubJoinRequestStatus Status { get; private set; }

        public ClubJoinRequest(long touristId, long clubId, DateTime requestedAt, ClubJoinRequestStatus status)
        {
            TouristId = touristId;
            ClubId = clubId;
            RequestedAt = requestedAt;
            Status = status;
            Validate();
        }

        private void Validate()
        {
            if (TouristId == 0) throw new ArgumentException("Invalid TouristId");
            if (ClubId == 0) throw new ArgumentException("Invalid ClubId");
            //if (RequestedAt > DateTime.Now) throw new ArgumentException("Invalid RequestedAt");
        }

        public void Respond(bool accepted)
        {
            if (Status == ClubJoinRequestStatus.Pending)
                Status = accepted ? ClubJoinRequestStatus.Accepted : ClubJoinRequestStatus.Rejected;
        }

        public void Cancel()
        {
            if (Status == ClubJoinRequestStatus.Pending)
                Status = ClubJoinRequestStatus.Cancelled;
        }

        public string GetPrimaryStatusName()
        {
            return Enum.GetName(typeof(ClubJoinRequestStatus), Status);
        }
    }

    public enum ClubJoinRequestStatus
    {
        Pending,
        Accepted,
        Rejected,
        Cancelled
    }
}


