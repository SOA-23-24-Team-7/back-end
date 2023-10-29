namespace Explorer.Tours.API.Dtos.TouristEquipment
{
    public class TouristEquipmentResponseDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public List<int>? EquipmentIds { get; set; }
    }
}
