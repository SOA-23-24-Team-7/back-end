using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class MessageResponseWithUsernamesDto
    {
        public long Id { get; set; }
        public long UserSenderId { get; set; }
        public string SenderUsername { get; set; }
        public long UserReciverId { get; set; }
        public string ReciverUsername { get; set; }
        public string Text { get; set; }
        public MessageStatus StatusOfMessage { get; set; }
    }
}
