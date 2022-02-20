using LivingLab.Core.Interfaces.Services;
using LivingLab.Core.Models.Prediction;
using LivingLab.ML.Model;

namespace LivingLab.ML;

public class EnergyPredictionService : IEnergyPredictionService
{
    public PredictionModelOutput Predict(PredictionModelInput input)
    {
        return MapToModelOutput(ConsumeModel.Predict(MapToModelInput(input)));
    }
    
    private ModelInput MapToModelInput(PredictionModelInput input)
    {
        return new ModelInput
        {
            DeviceType = input.DeviceType,
            DeviceSerialNo = input.DeviceSerialNo,
            EnergyUsage = input.EnergyUsage,
            Interval = input.Interval,
            LoggedAt = input.LoggedAt
        };
    }
    
    private PredictionModelOutput MapToModelOutput(ModelOutput output)
    {
        return new PredictionModelOutput
        {
            EnergyUsage = output.EnergyUsage
        };
    }
}
