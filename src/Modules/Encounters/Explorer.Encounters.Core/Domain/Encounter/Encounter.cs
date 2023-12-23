using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounter
{
    public class Encounter : Entity
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string Picture { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public double Radius { get; init; }
        public int XpReward { get; init; }
        public EncounterStatus Status { get; private set; }
        public EncounterType Type { get; init; }
        public List<EncounterInstance> Instances { get; } = new List<EncounterInstance>();

        public Encounter(string title, string description, string picture, double longitude, double latitude, double radius, int xpReward, EncounterStatus status, EncounterType type)
        {
            Title = title;
            Description = description;
            Picture = picture;
            Longitude = longitude;
            Latitude = latitude;
            Radius = radius;
            XpReward = xpReward;
            Radius = radius;
            Status = status;
            Type = type;
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Invalid Title");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
            if (string.IsNullOrWhiteSpace(Picture)) throw new ArgumentException("Invalid Picture");
            if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude");
            if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude");
            if (XpReward < 0) throw new ArgumentException("XP cannot be negative");
        }

        public void Archive()
        {
            Status = EncounterStatus.Archived;
        }

        public void Publish()
        {
            Status = EncounterStatus.Active;
        }

        public void CancelEncounter(long userId)
        {
            if (hasUserActivatedEncounter(userId))
            {
                var instance = Instances.Find(x => x.UserId == userId);
                if (instance!.Status == EncounterInstanceStatus.Completed) throw new ArgumentException("User has already completed this encounter.");
                Instances.Remove(instance);
            }
            throw new ArgumentException("User has not activated this encounter.");
        }

        public void ActivateEncounter(long userId, double userLongitude, double userLatitude)
        {
            if (Status != EncounterStatus.Active)
                throw new ArgumentException("Encounter is not yet published.");
            if (hasUserActivatedEncounter(userId))
                throw new ArgumentException("User has already activated/completed this encounter.");
            if (!isUserInRange(userLongitude, userLatitude))
                throw new ArgumentException("User is not close enough to the encounter.");

            Instances.Add(new EncounterInstance(userId));
        }

        public virtual void CompleteEncounter(long userId)
        {
            try
            {
                CompleteEncounterInstance(userId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        protected void CompleteEncounterInstance(long userId)
        {
            try
            {
                Instances.First(x => x.UserId == userId).Complete();
            }
            catch (Exception)
            {

                throw new ArgumentException("Invalid user id.");
            }
        }

        protected bool hasUserActivatedEncounter(long userId)
        {
            return Instances.FirstOrDefault(x => x.UserId == userId) != default(EncounterInstance);
        }

        protected bool isUserInRange(double userLongitute, double userLatitude)
        {
            if (userLongitute < -180 || userLongitute > 180) throw new ArgumentException("Invalid Longitude");
            if (userLatitude < -90 || userLatitude > 90) throw new ArgumentException("Invalid Latitude");

            const double earthRadius = 6371000;
            double latitude1 = Latitude * Math.PI / 180;
            double longitude1 = Longitude * Math.PI / 180;
            double latitude2 = userLatitude * Math.PI / 180;
            double longitude2 = userLongitute * Math.PI / 180;

            double latitudeDistance = latitude2 - latitude1;
            double longitudeDistance = longitude2 - longitude1;

            double a = Math.Sin(latitudeDistance / 2) * Math.Sin(latitudeDistance / 2) +
                       Math.Cos(latitude1) * Math.Cos(latitude2) *
                       Math.Sin(longitudeDistance / 2) * Math.Sin(longitudeDistance / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = earthRadius * c;

            return (distance < Radius);
        }
        public bool IsInRangeOf(double range, double userLongitute, double userLatitude)
        {
            if (userLongitute < -180 || userLongitute > 180) throw new ArgumentException("Invalid Longitude");
            if (userLatitude < -90 || userLatitude > 90) throw new ArgumentException("Invalid Latitude");

            const double earthRadius = 6371000;
            double latitude1 = Latitude * Math.PI / 180;
            double longitude1 = Longitude * Math.PI / 180;
            double latitude2 = userLatitude * Math.PI / 180;
            double longitude2 = userLongitute * Math.PI / 180;

            double latitudeDistance = latitude2 - latitude1;
            double longitudeDistance = longitude2 - longitude1;

            double a = Math.Sin(latitudeDistance / 2) * Math.Sin(latitudeDistance / 2) +
                       Math.Cos(latitude1) * Math.Cos(latitude2) *
                       Math.Sin(longitudeDistance / 2) * Math.Sin(longitudeDistance / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = earthRadius * c;

            return distance < range;
        }

    }
    public enum EncounterStatus { Active, Draft, Archived };
    public enum EncounterType { Social, Hidden, Misc, KeyPoint };

}