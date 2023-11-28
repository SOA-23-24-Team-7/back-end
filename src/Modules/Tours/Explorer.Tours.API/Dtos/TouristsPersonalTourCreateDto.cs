using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;
public class TouristsPersonalTourCreateDto
{
    public long TouristId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TourStatus Status { get; set; }
    public double Distance { get; set; }
}
