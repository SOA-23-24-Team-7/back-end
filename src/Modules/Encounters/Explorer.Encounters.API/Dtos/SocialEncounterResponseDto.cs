using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos;

public class SocialEncounterResponseDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int XpReward { get; set; }
    public EncounterStatus Status { get; set; }
}
