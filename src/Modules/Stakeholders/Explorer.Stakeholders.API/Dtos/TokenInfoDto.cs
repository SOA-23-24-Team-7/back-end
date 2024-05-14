using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TokenInfoDto
    {
        public long UserId { get; set; }
        public long PersonId { get; set; }
        public string Username { get; set; }
        public string Role {  get; set; }

        public TokenInfoDto(long userId, long presonId, string username, string role)
        {
            UserId = userId;
            PersonId = presonId;
            Username = username;
            Role = role;
        }
    }

   
}
