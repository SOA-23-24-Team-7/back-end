namespace Explorer.Stakeholders.API.Dtos.TouristEquipment
{
    public class TouristEquipmentUpdateDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public List<int> EquipmentIds { get; set; }
    }
}
