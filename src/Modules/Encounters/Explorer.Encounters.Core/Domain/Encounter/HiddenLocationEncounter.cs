namespace Explorer.Encounters.Core.Domain.Encounter
{
    public class HiddenLocationEncounter : Encounter
    {
        public string Picture { get; init; }
        public double PictureLongitude { get; init; }
        public double PictureLatitude { get; init; }

        public HiddenLocationEncounter(string picture, double pictureLongitude, double pictureLatitude, string title, string description, double longitude, double latitude, int xpReward, EncounterStatus status) : base(title, description, longitude, latitude, xpReward, status)
        {
            Picture = picture;
            PictureLongitude = pictureLongitude;
            PictureLatitude = pictureLatitude;
            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Picture)) throw new ArgumentException("Invalid Picture");
        }

    }
}
