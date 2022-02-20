using LivingLab.Core.Models.Prediction;

namespace LivingLab.Core.Interfaces.Services;

public interface IEnergyPredictionService
{
    PredictionModelOutput Predict(PredictionModelInput input);
}
