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

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet, HttpPost]
    public async Task<IActionResult> ViewUsage([FromBody] EnergyUsageFilterViewModel filter)
    {
        try
        {
            if (filter.Lab == null)
            {
                filter.Lab = new EnergyUsageLabViewModel { LabId = 1 };
            }
            var model = await _energyUsageService.GetEnergyUsage(filter);
            return Json(model);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return NotFound();
        }
    }
    
    public async Task<IActionResult> Benchmark(int labId = 1)
    {
        try
        {
            var benchmark = await _energyUsageService.GetLabEnergyBenchmark(labId);
            return View(benchmark);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return View();
        }
        
    }
    
    [HttpPost]
    public IActionResult Filter(EnergyUsageFilterViewModel filter)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> SetBenchmark(EnergyBenchmarkViewModel benchmark)
    {
        try
        {
           await _energyUsageService.SetLabEnergyBenchmark(benchmark);
           return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return RedirectToAction(nameof(Benchmark));
        }
    }
}
