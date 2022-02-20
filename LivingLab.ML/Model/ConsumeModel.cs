using LivingLab.Core.Interfaces.Services;
using LivingLab.Core.Models.Prediction;

using Microsoft.ML;

namespace LivingLab.ML.Model;

/// <summary>
/// Consumes the ML model to predict result.
/// </summary>
public class ConsumeModel
{
    private static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new(CreatePredictionEngine);

    public static string MLNetModelPath = Path.GetFullPath("Model/MLModel.zip");

    /// <summary>
    /// Call this method to predict the output of the model.
    /// </summary>
    /// 
    /// <param name="input">Model input value for prediction.</param>
    /// 
    /// <returns>Prediction Result</returns>
    public static ModelOutput Predict(ModelInput input)
    {
        ModelOutput result = PredictionEngine.Value.Predict(input);
        return result;
    }

    /// <summary>
    /// Creates the prediction engine.
    /// </summary>
    /// <returns></returns>
    public static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
    {
        // Create new MLContext
        MLContext mlContext = new MLContext();

        // Load model & create prediction engine
        ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var modelInputSchema);
        var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

        return predEngine;
    }
}
