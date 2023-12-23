namespace Explorer.Encounters.Core.Domain.Encounter
{
    public class HiddenLocationEncounter : Encounter
    {
        public double PictureLongitude { get; init; }
        public double PictureLatitude { get; init; }

        public HiddenLocationEncounter(double pictureLongitude, double pictureLatitude, string title, string description, string picture, double longitude, double latitude, double radius, int xpReward, EncounterStatus status, EncounterType type) : base(title, description, picture, longitude, latitude, radius, xpReward, status, type)
        {
            PictureLongitude = pictureLongitude;
            PictureLatitude = pictureLatitude;
        }


        public bool isUserInCompletionRange(double userLongitute, double userLatitude)
        {
            if (userLongitute < -180 || userLongitute > 180) throw new ArgumentException("Invalid Longitude");
            if (userLatitude < -90 || userLatitude > 90) throw new ArgumentException("Invalid Latitude");

            const double earthRadius = 6371000;
            double latitude1 = PictureLatitude * Math.PI / 180;
            double longitude1 = PictureLongitude * Math.PI / 180;
            double latitude2 = userLatitude * Math.PI / 180;
            double longitude2 = userLongitute * Math.PI / 180;

            double latitudeDistance = latitude2 - latitude1;
            double longitudeDistance = longitude2 - longitude1;

            double a = Math.Sin(latitudeDistance / 2) * Math.Sin(latitudeDistance / 2) +
                       Math.Cos(latitude1) * Math.Cos(latitude2) *
                       Math.Sin(longitudeDistance / 2) * Math.Sin(longitudeDistance / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = earthRadius * c;

            return distance < 10;
        }

    }
}
