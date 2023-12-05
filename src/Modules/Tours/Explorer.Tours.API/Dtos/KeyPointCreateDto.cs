namespace Explorer.Tours.API.Dtos
{
    public class KeyPointCreateDto
    {
        public long TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string LocationAddress { get; set; }
        public string ImagePath { get; set; }
        public long Order { get; set; }
        public KeyPointSecretDto? Secret { get; set; }
        public bool IsEncounterRequired { get; set; }
        public bool HasEncounter { get; set; }
    }
}
