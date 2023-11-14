using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Coordinate : ValueObject
    {
        public double Longitude { get; }
        public double Latitude { get; }

        public Coordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Validate();
        }

        private void Validate()
        {
            if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude");
            if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude");
        }

        public double CalculateDistance(double longitude, double latitude)
        {
            const double earthRadius = 6371000;

            double latitude1 = Latitude * Math.PI / 180;
            double longitude1 = Longitude * Math.PI / 180;
            double latitude2 = latitude * Math.PI / 180;
            double longitude2 = longitude * Math.PI / 180;

            double latitudeDistance = latitude2 - latitude1;
            double longitudeDistance = longitude2 - longitude1;

            double a = Math.Sin(latitudeDistance / 2) * Math.Sin(latitudeDistance / 2) +
                       Math.Cos(latitude1) * Math.Cos(latitude2) *
                       Math.Sin(longitudeDistance / 2) * Math.Sin(longitudeDistance / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = earthRadius * c;

            return distance;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
        }
    }
}