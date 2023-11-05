using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TouristEquipment
{
    public class TouristEquipmentUpdateDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public List<int> EquipmentIds { get; set; }
    }
}
