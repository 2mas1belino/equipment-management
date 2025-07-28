using System.Text.Json.Serialization;

namespace EquipmentManagement.Shared.Dtos
{
    public class EquipmentCreateDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
