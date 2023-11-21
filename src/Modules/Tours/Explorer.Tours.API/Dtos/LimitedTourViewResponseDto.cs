using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class LimitedTourViewResponseDto
    {
        public int Id { get; set; }
        public long AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public List<string> Tags { get; set; }
        public double Price { get; set; }
        public double Distance { get; set; }
        public KeyPointResponseDto? KeyPoint { get; set; }
        public List<TourDurationResponseDto>? Durations { get; set; }
        public List<ReviewResponseDto>? Reviews { get; set; }
      
    }
}
