using System.Diagnostics;
using System.Data;
using System.Text;

using LivingLab.Web.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace LivingLab.Web.Controllers;

[Route("dashboard")]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ILogger<DashboardController> logger)
    {
        _logger = logger;
    }

    [Route("index")]
    public IActionResult Index()
    {
        List<Log> Logs = logList();
        ViewBag.Logs = Logs;
        return View();
    }

    [Route("export")]
    public IActionResult Export()
    {

        var builder = new StringBuilder();
        List<Log> Logs = logList();
        builder.AppendLine("Device SerialNo,Device Type,Toatl Energy Usage,Energy Usage Cost,Energy Usage Intensity");
        foreach (var log in Logs)
        {
            builder.AppendLine($"{log.DeviceSerialNo},{log.DeviceType},{log.ToatlEnergyUsage},{log.EnergyUsageCost},{log.EnergyUsageIntensity}");
        }

        return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Device Energy Usage.csv");
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public struct Log{
        public Log (String DeviceserialNo, String Devicetype, int Toatlenergyusage, double Energyusagecost, double Energyusageintensity) {
            DeviceSerialNo = DeviceserialNo;
            DeviceType = Devicetype;
            ToatlEnergyUsage = Toatlenergyusage;
            EnergyUsageCost = Energyusagecost;
            EnergyUsageIntensity = Energyusageintensity;
        } 
        public String DeviceSerialNo { get; }
        public String DeviceType { get; }
        public int ToatlEnergyUsage { get; }
        public double EnergyUsageCost { get; }
        public double EnergyUsageIntensity { get; }
    }

    public List<Log> logList() {
        List<Log> Logs = new List<Log>();
        Logs.Add(new Log("Sensor-12120","sensor",234,23.21,43.2));
        Logs.Add(new Log("Actuator-0881","Actuator",121,13.45,21.2));
        Logs.Add(new Log("Robot-73","Robot",691,83.45,21.2));

        return Logs;
    }


} 






