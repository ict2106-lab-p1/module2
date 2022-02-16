
namespace LivingLab.Domain.Entities;
using Microsoft.Build.Framework;


public class DeviceDTO
{
    public DateTime ValidityDate { get; set; }

    public string? serialNo { get; set; }
    public Lab Lab { get; set; }
    public DeviceType DeviceType { get; set; }
}
