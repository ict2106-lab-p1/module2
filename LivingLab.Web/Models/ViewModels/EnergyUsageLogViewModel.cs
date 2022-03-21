using LivingLab.Core.Entities.Identity;
using LivingLab.Web.Models.ViewModels.Device;
using LivingLab.Web.Models.ViewModels.EnergyUsage;

namespace LivingLab.Web.Models.ViewModels;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
public class EnergyUsageLogViewModel
{
    public double EnergyUsage { get; set; }
    public TimeSpan Interval { get; set; }
    public DateTime LoggedDate { get; set; }
    public EnergyUsageLabViewModel Lab { get; set; }
    public DeviceViewModel Device { get; set; }
}
