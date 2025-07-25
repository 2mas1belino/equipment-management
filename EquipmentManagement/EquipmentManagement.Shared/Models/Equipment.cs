namespace EquipmentManagement.Shared.Models;

public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? Description { get; set; }
    public byte[]? ImageData { get; set; }
    public string? ImageContentType { get; set; }
    public List<AvailabilityPeriod> AvailabilityPeriods { get; set; } = new();
}
