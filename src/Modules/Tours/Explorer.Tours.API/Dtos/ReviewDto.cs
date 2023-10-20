using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public long TouristId { get; set; }
        public DateOnly TourVisitDate { get; set; }
        public DateOnly CommentDate { get; set; }
        public int TourId { get; set; }
        public List<string> Images {  get; set; }
    }
}
