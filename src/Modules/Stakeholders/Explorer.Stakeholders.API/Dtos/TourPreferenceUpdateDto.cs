using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TourPreferenceUpdateDto
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int DifficultyLevel { get; set; }
        public int WalkingRating { get; set; }
        public int CyclingRating { get; set; }
        public int CarRating { get; set; }
        public int BoatRating { get; set; }
        public List<string> SelectedTags { get; set; }
    }
}
