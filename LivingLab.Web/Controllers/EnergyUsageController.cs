using System.Diagnostics;

using LivingLab.Web.Models.ViewModels;
using LivingLab.Web.Models.ViewModels.EnergyUsage;
using LivingLab.Web.UIServices.EnergyUsage;
using LivingLab.Web.UIServices.LabProfile;

using Microsoft.AspNetCore.Mvc;

namespace LivingLab.Web.Controllers;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
public class EnergyUsageController : Controller
{
    private readonly IEnergyUsageService _energyUsageService;
    private readonly ILabProfileService _labProfileService;
    private readonly ILogger<EnergyUsageController> _logger;
    
    public EnergyUsageController(IEnergyUsageService energyUsageService, ILabProfileService labProfileService, ILogger<EnergyUsageController> logger)
    {
        _energyUsageService = energyUsageService;
        _labProfileService = labProfileService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var labs = await _labProfileService.GetAllLabAccounts();
        return View(labs.labList);
    }
    
    public IActionResult Lab(int? LabId = 1)
    {
        ViewBag.LabId = LabId;
        return View();
    }
    
    
    [HttpPost]
    public async Task<IActionResult> GetLabUsage([FromBody] EnergyUsageFilterViewModel filter)
    {
        try
        {
            var model = await _energyUsageService.GetEnergyUsage(filter);
            return model.Lab != null ? Json(model) : NotFound();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return NotFound();
        }
    }

    [HttpGet("EnergyUsage/Benchmark/Lab/{labId?}")]
    public async Task<IActionResult> Benchmark(int? labId = 1)
    {
        try
        {
            var benchmark = await _energyUsageService.GetLabEnergyBenchmark(labId!.Value);
            return benchmark != null ? View(benchmark) : NotFound();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Error();
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> SetBenchmark(EnergyBenchmarkViewModel benchmark)
    {
        try
        {
           await _energyUsageService.SetLabEnergyBenchmark(benchmark);
           return RedirectToAction(nameof(Index), new {labId = benchmark.LabId});
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return RedirectToAction(nameof(Benchmark));
        }
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
