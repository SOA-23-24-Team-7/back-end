using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Problems
{
    public class ProblemResolvingNotification : Entity
    {
        public long ProblemId { get; init; }
        public long ReceiverId { get; init; }
        public long SenderId { get; init; }
        public User Sender { get; init; }
        public string Message { get; init; }
        public DateTime Created { get; init; }
        public bool HasSeen { get; init; } = false;

        public ProblemResolvingNotification(long problemId, long receiverId, long senderId, string message, DateTime created)
        {
            ProblemId = problemId;
            ReceiverId = receiverId;
            SenderId = senderId;
            Message = message;
            Created = created;
            Validate(message);
        }

        private void Validate(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Invalid message.");
        }
    }
}
