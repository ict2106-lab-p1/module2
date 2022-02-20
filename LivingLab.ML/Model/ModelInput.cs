using Microsoft.ML.Data;

namespace LivingLab.ML.Model;

public class ModelInput
{
    [LoadColumn(0)]
    public string DeviceType { get; set; }
    
    [LoadColumn(1)]
    public string DeviceSerialNo { get; set; }

    [LoadColumn(2)]
    public float EnergyUsage { get; set; }

    [LoadColumn(3)]
    public float Interval { get; set; }

    [LoadColumn(4)]
    public string LoggedAt { get; set; }
}
