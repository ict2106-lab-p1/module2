using System.Globalization;

using CsvHelper;

namespace LivingLab.DataGenerator;
/// <summary>
/// Generates a csv file with mock energy usage records.
/// </summary>
public static class EnergyUsageGenerator
{
    /// <summary>
    /// Generate mock data for energy usage log and
    /// inserts them into a csv file.
    /// </summary>
    public static void GenerateEnergyUsageCsv()
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, "Data", "energy-usage.csv");
        
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        var records = CreateRecords();
        
        Console.WriteLine("Generating CSV file...");
        csv.WriteHeader<EnergyUsageLog>();
        csv.NextRecord();
        csv.WriteRecords(records);
        Console.WriteLine($"Successfully generated {records.Count} records into CSV file.");
    }
    

    /// <summary>
    /// Generate each log entry based on device type and interval.
    /// </summary>
    /// 
    /// <remarks>
    /// Current Start and End date is set to:
    /// Start Date: 10 Feb 2022 0000H
    /// End Date: 20 Feb 2022 2359H
    /// </remarks>
    ///
    /// <returns>List of Energy Usage Logs</returns>
    private static List<EnergyUsageLog> CreateRecords()
    {
        var records = new List<EnergyUsageLog>();
        var startDate = new DateTime(2022, 02, 10);
        var endDate = new DateTime(2022, 02, 20, 23, 59, 00);
        
        var currentDate = startDate;
        var devices = DeviceBuilder.GetDevices();

        foreach (var log in devices.Select(kvp => kvp.Value))
        {
            while (currentDate < endDate)
            {
                records.Add(new EnergyUsageLog
                {
                    DeviceType = log.DeviceType,
                    SerialNo = log.SerialNo,
                    EnergyUsage = GenerateRandomValue(IsPeakHour(currentDate.Hour)),
                    Interval = log.Interval,
                    LoggedAt = currentDate
                });
                
                currentDate = currentDate.AddMinutes(log.Interval);
            }
            currentDate = startDate;
        }

        return records;
    }
    
    
    /// <summary>
    /// Check if timing is between 9am to 6pm (working hours).
    /// </summary>
    ///
    /// <param name="hour">Hour of the current time.</param>
    ///
    /// <returns>True if hour is within working hours, false otherwise.</returns>
    /// 
    private static Boolean IsPeakHour(int hour)
    {
        return hour is >= 9 and <= 18;
    }
    
    
    /// <summary>
    /// Generate a larger number during peak hour to
    /// simulate high energy usage.
    /// </summary>
    ///
    /// <param name="isPeak">States whether it is peak hour</param>
    ///
    /// <returns>Random value based on whether it is peak hour</returns>
    private static float GenerateRandomValue(Boolean isPeak)
    {
        var random = new Random();
        return isPeak ? random.Next(5000, 5500) : random.Next(3000, 3500);
    }
}
