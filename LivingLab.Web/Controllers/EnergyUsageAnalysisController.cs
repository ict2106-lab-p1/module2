using System.Diagnostics;

using LivingLab.Core.Entities.DTO.EnergyUsageDTOs;

using Microsoft.AspNetCore.Mvc;
using LivingLab.Core.Interfaces.Repositories;
using LivingLab.Core.Entities;
using LivingLab.Web.Models.ViewModels;
using LivingLab.Web.Models.ViewModels.EnergyUsage;
using LivingLab.Web.UIServices.EnergyUsage;
using LivingLab.Web.UIServices.LabProfile;

namespace LivingLab.Web.Controllers;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
public class EnergyUsageAnalysisController : Controller
{
    private readonly ILogger<EnergyUsageAnalysisController> _logger;
    private readonly IEnergyUsageRepository _repository;
    private readonly IEnergyUsageAnalysisUIService _analysisService;
    public EnergyUsageAnalysisController(ILogger<EnergyUsageAnalysisController> logger, IEnergyUsageRepository repository, IEnergyUsageAnalysisUIService analysisService)
    {
        _logger = logger;
        _repository = repository;
        _analysisService = analysisService;
    }
    public async Task<IActionResult> Index(string? LabLocation = "NYP-SR7C")
    {
        // List<Log> Logs = logList();
        List<DeviceEnergyUsageDTO> Logs = DeviceEUList1();
        ViewBag.Logs = Logs;
        ViewBag.LabLocation = LabLocation;
        //var labs = await _analysisService.GetAllLabs();
        return View();
        // GetAll();
    }
    
    // public IActionResult Lab(int? LabId = 1)
    // {
    //     ViewBag.LabId = LabId;
    //     return View();
    // }
    
    [HttpGet]
    public IActionResult Export()
    {
        List<DeviceEnergyUsageDTO> Logs = DeviceEUList();
        byte [] content =  _analysisService.Export();
        return File(content, "text/csv", "Device Energy Usage.csv");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public List<DeviceEnergyUsageDTO> DeviceEUList() 
    {
        var Logs = new List<DeviceEnergyUsageDTO>(){
            new DeviceEnergyUsageDTO{DeviceSerialNo="Sensor-12120",DeviceType="Sensor",TotalEnergyUsage=234,EnergyUsagePerHour=112,EnergyUsageCost=23.21},
            new DeviceEnergyUsageDTO{DeviceSerialNo="Actuator-0881",DeviceType="Actuator",TotalEnergyUsage=121,EnergyUsagePerHour=23,EnergyUsageCost=12.21},
            new DeviceEnergyUsageDTO{DeviceSerialNo="Robot-73",DeviceType="Robot",TotalEnergyUsage=671,EnergyUsagePerHour=211,EnergyUsageCost=72.45}
        };
        return Logs;
    }

    public List<DeviceEnergyUsageDTO> DeviceEUList1() 
    {
        DateTime start = new DateTime(2015, 12, 25);
        DateTime end = new DateTime(2022, 12, 25);
        return _analysisService.GetDeviceEnergyUsageByDate(start,end);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var records = _repository.GetAllAsync().Result;
                // var records = DeviceEUList();
        return Ok(records);
    }
    public void GetAlla()
    {
        var records = _repository.GetAllAsync().Result;
        var AllDevices = FindAllUniqueID(records);
        foreach (var item in AllDevices)
        {
            Console.WriteLine(item);
        }
    }

    public List<int> FindAllUniqueID (List<EnergyUsageLog> Records) 
    {
        List<int> IdList = new List<int>();
        foreach (var item in Records)
        {
            if (!IdList.Contains(item.Device.Id))
            {
                IdList.Add(item.Device.Id);
            } 
        }
        return IdList;
    }

    // convert time in minutes to hour
    public double ConvertTimeToHour(int TimeInMinute) 
    {
        return (double)TimeInMinute / (double)60;
    }
    // calculate energy usage per hour
    public double? CalculateEUPerHour (double? TotalEU, int? TotalEUTime) 
    {
        double? hour = (double)TotalEUTime / (double)60;
        double? EUPerHour = TotalEU / hour;
        return (int)EUPerHour;
    }

    // calculate total energy usage cost
    public double CalculateEUCost(double cost, int TotalEU, double TotalEUTime) 
    {
        double Total = Math.Round((cost * (double)TotalEU * TotalEUTime),2);
        return Total;
    }
    
    // I think joey need use this 
    // public IActionResult GetLabEnergyUsageDetailGraph(string listOfLabId, DateTime start, DateTime end)
    // {
    //     return View();
    // }
    // or this
    // public IActionResult GetLabEnergyUsageByDate(DateTime start, DateTime end)
    // {
    //     return View();
    // }
    
    // public IActionResult GetLabEnergyUsageByDate(EnergyUsageFilterDTO filter)
    // {
    //     return View();
    // }
    
    // public IActionResult ViewUsage(EnergyUsageTrendAllLabViewModel usage)
    // {
    //     return View();
    // }

    [HttpPost]
    public async Task<IActionResult> ViewUsage([FromBody] EnergyUsageFilterViewModel filter)
    {
        try
        {
            var model = await _analysisService.GetEnergyUsageTrendSelectedLab(filter);
            return model.Lab != null ? Json(model) : NotFound();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return NotFound();
        }
    }

    // [HttpGet("EnergyUsageAnalysis/Benchmark/Lab/{labId?}")]
    public async Task<IActionResult> Benchmark(int? labId = 1)
    {
        try
        {
            var benchmark = await _analysisService.GetLabEnergyBenchmark(labId!.Value);
            return benchmark != null ? View(benchmark) : NotFound();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Error();
        }
        
    }
} 






