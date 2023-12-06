using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterInstanceResponseDto
    {
        public long UserId { get; set; }
        public EncounterInstanceStatus Status { get; set; }
        public DateTime? CompletionTime { get; set; }
    }
    public enum EncounterInstanceStatus { Active, Completed }
}
