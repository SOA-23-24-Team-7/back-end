using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CampaignResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public double AverageDifficulty { get; set; }
        public ICollection<EquipmentResponseDto> Equipments { get; set; }
        public ICollection<KeyPointResponseDto> KeyPoints { get; set; }
    }
}
