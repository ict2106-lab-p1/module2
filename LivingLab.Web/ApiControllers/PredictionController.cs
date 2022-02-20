using LivingLab.Core.Interfaces.Services;
using LivingLab.Core.Models.Prediction;

using Microsoft.AspNetCore.Mvc;

namespace LivingLab.Web.ApiControllers;

public class PredictionController : BaseApiController
{
    private readonly IEnergyPredictionService _predictionService;

    public PredictionController(IEnergyPredictionService predictionService)
    {
        _predictionService = predictionService;
    }
    
    [HttpPost]
    public IActionResult Predict(PredictionModelInput input)
    {
        var result = _predictionService.Predict(input);
        return Ok(result.EnergyUsage);
    }
}
