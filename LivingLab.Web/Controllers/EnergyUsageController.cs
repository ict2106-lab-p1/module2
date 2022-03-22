using System.Diagnostics;

using LivingLab.Web.Models.ViewModels;
using LivingLab.Web.Models.ViewModels.EnergyUsage;
using LivingLab.Web.UIServices.EnergyUsage;

using Microsoft.AspNetCore.Mvc;

namespace LivingLab.Web.Controllers;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
public class EnergyUsageController : Controller
{
    private readonly IEnergyUsageService _energyUsageService;
    private readonly ILogger<EnergyUsageController> _logger;
    
    public EnergyUsageController(IEnergyUsageService energyUsageService, ILogger<EnergyUsageController> logger)
    {
        _energyUsageService = energyUsageService;
        _logger = logger;
    }

    [HttpGet]
    [Route("EnergyUsage/Lab/{labId?}")]
    public IActionResult Index(int? labId = 1)
    {
        ViewBag.LabId = labId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ViewUsage([FromBody] EnergyUsageFilterViewModel filter)
    {
        try
        {
            var model = await _energyUsageService.GetEnergyUsage(filter);
            return Json(model);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return NotFound();
        }
    }

    [Route("EnergyUsage/Benchmark/Lab/{labId?}")]
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
