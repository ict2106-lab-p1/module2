namespace LivingLab.DataGenerator;

/// <summary>
/// Object to hold data to be inserted into energy usage csv file.
/// </summary>
public class EnergyUsageLog
{
    public string DeviceType { get; set; }
    
    public string SerialNo { get; set; }

    public float EnergyUsage { get; set; }

    public float Interval { get; set; }

    public DateTime LoggedAt { get; set; }
}
