﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos;

public class ClubInvitationWithClubAndOwnerName
{
    public long Id { get; set; }
    public string? ClubName { get; set; }
    public string? OwnerUsername { get; set; }
}
