using System.ComponentModel.DataAnnotations;

namespace LivingLab.Web.ViewModels;

public class DeviceEnergyUsageViewModel
{
    [Display(Name = "Serial No.")]
    public string? DeviceSerialNo { get; set; }

    [Display(Name = "Type")]
    public string? DeviceType { get; set;}

    [Display(Name = "Total Energy Usage")]
    public int? TotalEnergyUsage { get; set;}

    [Display(Name = "Energy Usage Per Hour")]
    public int? EnergyUsagePerHour { get; set;}

    [Display(Name = "Total Cost")]
    public double EnergyUsageCost { get; set;}
}