using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public enum MessageStatus { Seen, NotSeen};
    public class MessageDto
    {
        public int MessageId { get; set; } 
        public UserResponseDto UserSender { get; set; }
        public UserResponseDto UserReciver { get; set; }
        public string Text { get; set; }
        public MessageStatus StatusOfMessage { get; set; }
    }
}
