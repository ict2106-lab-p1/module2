using LivingLab.Core.Constants;

namespace LivingLab.DataGenerator;

/// <summary>
/// A builder to create devices
/// </summary>
public static class DeviceBuilder
{
    /// <summary>
    /// Generates a dictionary and associate each device type and interval.
    /// </summary>
    ///
    /// <returns>Dictionary of devices with device serial number as key</returns>
    internal static Dictionary<string, EnergyUsageLog> GetDevices()
    {
        return new Dictionary<string, EnergyUsageLog>
        {
            { DummyDevices.SMART_SENSOR_DEVICE_1, new EnergyUsageLog
            {
                SerialNo = DummyDevices.SMART_SENSOR_DEVICE_1,
                Interval = 5,
                DeviceType = DeviceTypes.SMART_SENSOR
            } }, 
            { DummyDevices.SMART_SENSOR_DEVICE_2, new EnergyUsageLog
            {
                SerialNo = DummyDevices.SMART_SENSOR_DEVICE_2,
                Interval = 10,
                DeviceType = DeviceTypes.SMART_SENSOR
            } },
            { DummyDevices.SMART_LIGHTING_DEVICE_1, new EnergyUsageLog
            {
                SerialNo = DummyDevices.SMART_LIGHTING_DEVICE_1,
                Interval = 10,
                DeviceType = DeviceTypes.SMART_LIGHTING
            } },
            { DummyDevices.SMART_LIGHTING_DEVICE_2, new EnergyUsageLog
            {
                SerialNo = DummyDevices.SMART_LIGHTING_DEVICE_2,
                Interval = 15,
                DeviceType = DeviceTypes.SMART_LIGHTING
            } }
        };
    }
}
