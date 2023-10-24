using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain
{
    public class Problem : Entity
    {
        public string Category { get; init; }
        public string Priority { get; init; }
        public string Description { get; init; }
        public string ReportedTime { get; init; }
        public int TouristId { get; init; }
        public int TourId { get; init; }

        public Problem(string category, string priority, string description, string reportedTime, int touristId, int tourId)
        {
            Validate(category, priority, description,reportedTime);
            Category = category;
            Priority = priority;
            Description = description;
            ReportedTime = reportedTime;
            TouristId = touristId;
            TourId = tourId;
        }
        public void Validate(string category, string priority, string description, string reportedTime)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Invalid Category.");
            if (string.IsNullOrWhiteSpace(priority)) throw new ArgumentException("Invalid Priority.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrEmpty(reportedTime)) throw new ArgumentException("Invalid Reported Time.");
        }
    }
}
