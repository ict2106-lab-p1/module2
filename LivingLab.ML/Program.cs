using System.Globalization;

using LivingLab.Core.Constants;
using LivingLab.ML.Model;

// Uncomment to generate model
// ModelBuilder.CreateModel();

var sampleData = new ModelInput
{
    DeviceType = DeviceTypes.SMART_SENSOR,
    DeviceSerialNo = DummyDevices.SMART_SENSOR_DEVICE_1,
    Interval = 5F,
    LoggedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture)
};

var predictionResult = ConsumeModel.Predict(sampleData);

Console.WriteLine("Using model to make single prediction -- Comparing actual EnergyUsage with predicted EnergyUsage from sample data...\n\n");
Console.WriteLine($"DeviceType: {sampleData.DeviceType}");
Console.WriteLine($"SerialNo: {sampleData.DeviceSerialNo}");
Console.WriteLine($"Interval: {sampleData.Interval}");
Console.WriteLine($"LoggedAt: {sampleData.LoggedAt}");
Console.WriteLine($"\n\nPredicted EnergyUsage: {predictionResult.EnergyUsage}\n\n");
Console.WriteLine("=============== End of process, hit any key to finish ===============");
Console.ReadKey();


