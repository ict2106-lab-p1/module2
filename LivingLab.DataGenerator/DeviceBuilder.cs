using LivingLab.Core.Constants;

namespace LivingLab.DataGenerator;

/// <summary>
/// A builder to create devices
/// </summary>
public static class DeviceBuilder
{
    const string SMART_SENSOR_DEVICE_1 = "DEVICE-1598";
    const string SMART_SENSOR_DEVICE_2 = "DEVICE-3390";
    const string SMART_LIGHTING_DEVICE_1 = "DEVICE-9987";
    const string SMART_LIGHTING_DEVICE_2 = "DEVICE-9988";
    
    /// <summary>
    /// Generates a dictionary and associate each device type and interval.
    /// </summary>
    ///
    /// <returns>Dictionary of devices with device serial number as key</returns>
    internal static Dictionary<string, EnergyUsageLog> GetDevices()
    {
        return new Dictionary<string, EnergyUsageLog>
        {
            { SMART_SENSOR_DEVICE_1, new EnergyUsageLog
            {
                SerialNo = SMART_SENSOR_DEVICE_1,
                Interval = 5,
                DeviceType = DeviceTypes.SMART_SENSOR
            } }, 
            { SMART_SENSOR_DEVICE_2, new EnergyUsageLog
            {
                SerialNo = SMART_SENSOR_DEVICE_2,
                Interval = 10,
                DeviceType = DeviceTypes.SMART_SENSOR
            } },
            { SMART_LIGHTING_DEVICE_1, new EnergyUsageLog
            {
                SerialNo = SMART_LIGHTING_DEVICE_1,
                Interval = 10,
                DeviceType = DeviceTypes.SMART_LIGHTING
            } },
            { SMART_LIGHTING_DEVICE_2, new EnergyUsageLog
            {
                SerialNo = SMART_LIGHTING_DEVICE_2,
                Interval = 15,
                DeviceType = DeviceTypes.SMART_LIGHTING
            } }
        };
    }
}
