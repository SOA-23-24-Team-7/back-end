using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class FollowingResponseWithUserDto
    {
        public long Id { get; set; }
        public UserResponseDto Following { get; set; }
        public PersonResponseDto FollowingPerson { get; set; }
    }
}
