
namespace Explorer.Encounters.API.Dtos
{
    public class EncounterFilterDto
    {

       
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? Radius { get; set; }
        public int? minXpReward { get; set; }
        public int? maxXpReward { get; set; }
        public EncounterStatus? Status { get; set; }
        public EncounterType? Type { get; set; }


    }

    public enum SortOption
    {
        NoSort = 0,
        XpRewardAsc,
        XpRewardDesc
    }

}
