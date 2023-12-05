using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Problems
{
    public class ProblemResolvingNotification : Entity
    {
        public string Header { get; init; }
        public long ProblemId { get; init; }
        public long ReceiverId { get; init; }
        public long SenderId { get; init; }
        public User Sender { get; init; }
        public string Message { get; init; }
        public DateTime Created { get; init; }
        public bool HasSeen { get; private set; } = false;

        public ProblemResolvingNotification(long problemId, long receiverId, long senderId, string message, DateTime created, string header)
        {
            ProblemId = problemId;
            ReceiverId = receiverId;
            SenderId = senderId;
            Message = message;
            Created = created;
            Header = header;
            Validate(message, header);
        }

        public void SetSeenStatus()
        {
            HasSeen = true;
        }

        private void Validate(string message, string header)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Invalid message.");
            if (string.IsNullOrWhiteSpace(header)) throw new ArgumentException("Invalid header.");
        }
    }
}
