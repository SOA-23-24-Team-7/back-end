namespace Explorer.Stakeholders.API.Dtos
{
    public class TouristEquipmentDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public List<int>? EquipmentIds { get; set; }
    }
}
