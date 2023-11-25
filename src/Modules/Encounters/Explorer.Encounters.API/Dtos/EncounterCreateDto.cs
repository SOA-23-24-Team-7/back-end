namespace Explorer.Encounters.API.Dtos
{
    public class EncounterCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public long Xp { get; set; }
        public EncounterStatus Status { get; set; }
        public EncounterType Type { get; set; }
    }
    public enum EncounterStatus { Active, Draft, Archieved };
    public enum EncounterType { Social, Location, Misc };
}
