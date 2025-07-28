namespace EquipmentManagement.Shared.Models;
public class AvailabilityPeriod
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int EquipmentId { get; set; }
    public Equipment? Equipment { get; set; }
}
