using System.Text.Json.Serialization;

namespace Explorer.Tours.API.Dtos;

public class EquipmentResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}