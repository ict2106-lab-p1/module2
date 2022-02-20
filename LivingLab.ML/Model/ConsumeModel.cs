using Microsoft.ML;

namespace LivingLab.ML.Model;

public class ConsumeModel
{
    private static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new(CreatePredictionEngine);

    public static string MLNetModelPath = Path.GetFullPath("Data/MLModel.zip");

    public static ModelOutput Predict(ModelInput input)
    {
        ModelOutput result = PredictionEngine.Value.Predict(input);
        return result;
    }

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
