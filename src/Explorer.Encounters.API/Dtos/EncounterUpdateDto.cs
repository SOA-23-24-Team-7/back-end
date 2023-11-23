namespace Explorer.Encounters.API.Dtos
{
    public class EncounterUpdateDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public long Xp { get; set; }
        public EncounterStatus Status { get; set; }
        public EncounterType Type { get; set; }
    }
}
