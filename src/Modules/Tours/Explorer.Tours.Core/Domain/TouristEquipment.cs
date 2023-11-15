using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TouristEquipment : Entity
    {
        public int TouristId { get; init; }
        public List<int> EquipmentIds { get; init; }
        public TouristEquipment(int touristId, List<int> equipmentIds)
        {
            TouristId = touristId;
            EquipmentIds = equipmentIds;
        }
    }
}