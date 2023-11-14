using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class PublicKeyPoint : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public string ImagePath { get; init; }
        public long Order { get; init; }
        public string LocationAddress { get; init; }

        public PublicKeyPoint(string name, string description, double longitude, double latitude, string imagePath, long order, string locationAddress)
        {
            Name = name;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
            ImagePath = imagePath;
            Order = order;
            Validate();
            LocationAddress = locationAddress;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
            if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude");
            if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude");
            if (string.IsNullOrWhiteSpace(ImagePath)) throw new ArgumentException("Invalid ImagePath");
        }

    }
}
