using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class PublicKeyPointNotification : Entity
    {
        public long RequestId { get; init; }
        public string Description { get; set; }
        public long AuthorId { get; init; }
        public DateTime Created { get; set; }
        public bool IsAccepted { get; init; }
        public string Comment { get; init; }
        public bool isSeen { get; private set; }
        public PublicKeyPointNotification(string description, long authorId, long requestId, DateTime created, bool isAccepted, string comment)
        {
            Validate(description);
            Description = description;
            AuthorId = authorId;
            RequestId = requestId;
            Created = created;
            IsAccepted = isAccepted;
            Comment = comment;
        }
        private void Validate(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
        }

        public void SetSeenStatus()
        {
            isSeen = true;
        }
    }
}
