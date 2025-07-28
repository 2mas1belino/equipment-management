using System.Text.Json.Serialization;

namespace EquipmentManagement.Shared.Dtos
{
    public class EquipmentDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("imageContentType")]
        public string? ImageContentType { get; set; }

        [JsonPropertyName("availabilityPeriods")]
        public List<AvailabilityPeriodDto> AvailabilityPeriods { get; set; } = new();
    }
}
