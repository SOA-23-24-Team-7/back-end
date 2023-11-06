using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum MessageStatus { Seen, NotSeen };
    public class Message:Entity
    {
        public int MessageID { get; set; }
        public UserResponseDto UserSender { get; set; }
        public UserResponseDto UserReciver { get; set; }
        public string Text { get; set; }
        public MessageStatus StatusOfMessage { get; set; }
    }
}
