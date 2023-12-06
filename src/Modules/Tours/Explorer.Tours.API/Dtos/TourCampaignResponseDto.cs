using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourCampaignResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Difficulty { get; set; }
        public double Price { get; set; }
        public double Distance { get; set; }
        public List<KeyPointResponseDto> KeyPoints { get; set; }
        public List<EquipmentResponseDto> Equipments { get; set; }
        public List<TourDurationResponseDto> Durations { get; set; }
    }
}
