using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Shared.Models;

public class Equipment
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Model { get; set; } = string.Empty;
    public string? Description { get; set; }
    public byte[]? ImageData { get; set; }
    public string? ImageContentType { get; set; }
    public List<AvailabilityPeriod> AvailabilityPeriods { get; set; } = new();
}
