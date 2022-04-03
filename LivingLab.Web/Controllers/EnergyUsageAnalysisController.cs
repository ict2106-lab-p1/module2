using System.Diagnostics;

using LivingLab.Core.Entities.DTO.EnergyUsageDTOs;

using Microsoft.AspNetCore.Mvc;
using LivingLab.Core.Interfaces.Repositories;
using LivingLab.Core.Entities;
using LivingLab.Web.Models.ViewModels;
using LivingLab.Web.UIServices.EnergyUsage;
using LivingLab.Web.Models.ViewModels.EnergyUsage;


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

    public IActionResult Index()
    {
        return View(data());
    }

    public IActionResult DMoreData()
    {
        ViewBag.Logs = "-";
        return View();
    }

    public IActionResult LMoreData()
    {
        ViewBag.Logs = "-";
        return View();
    }

    [HttpGet]
    public IActionResult Export()
    {
        byte [] content =  _analysisService.Export(data().DeviceEUList);
        return File(content, "text/csv", "Device Energy Usage.csv");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public EnergyUsageAnalysisViewModel data() {
        DateTime start = new DateTime(2015, 12, 25);
        DateTime end = new DateTime(2022, 12, 25);
        var deviceEUList = _analysisService.GetDeviceEnergyUsageByDate(start,end);
        var labEUList = _analysisService.GetLabEnergyUsageByDate(start,end);
        var viewModel = new EnergyUsageAnalysisViewModel {
            DeviceEUList = deviceEUList,
            LabEUList = labEUList
        };
        return viewModel;
    }

} 






