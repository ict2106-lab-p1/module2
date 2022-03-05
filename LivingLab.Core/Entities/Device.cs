using System.ComponentModel.DataAnnotations;

namespace LivingLab.Core.Entities;
public class Device : BaseEntity
{
    [Required]
    public string? DeviceSerialNumber { get; set; }
    [Required]
    // NOTE use one of the constants from LivingLab.Core.Constants.DeviceTypes
    public string? Type { get; set; }
    public double? EnergyUsageThreshold { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime LastUpdated { get; set; }

    public int LabId { get; set; }
    
    [Required]
    public Lab Lab { get; set; }
    [Required]
    public string Status { get; set; }
    
    public string? Description { get; set; }
}
