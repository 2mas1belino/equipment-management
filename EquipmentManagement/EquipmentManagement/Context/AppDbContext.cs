using EquipmentManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<AvailabilityPeriod> AvailabilityPeriods { get; set; }
}