using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;
public class TouristsPersonalTourResponseDto
{
    public int Id { get; set; }
    public long TouristId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // public int Difficulty { get; set; }
    // public List<string> Tags { get; set; }
    public TourStatus Status { get; set; }
    public double Distance { get; set; }
    public List<TourDurationUpdateDto> Durations { get; set; }
}
