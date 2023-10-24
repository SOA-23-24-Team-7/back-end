using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos;

public class ClubInvitationWithUsernameDto
{
    public string Username { get; set; }
    public long ClubId { get; set; }
}
