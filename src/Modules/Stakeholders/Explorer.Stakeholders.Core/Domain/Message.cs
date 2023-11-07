using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum MessageStatus { NotSeen, Seen };
    public class Message:Entity
    {
        public long UserSenderId { get; set; }
        public User UserSender { get; set; }
        public long UserReciverId { get; set; }
        public User UserReciver { get; set; }
        public string Text { get; set; }
        public MessageStatus StatusOfMessage { get; set; }
    }
}
