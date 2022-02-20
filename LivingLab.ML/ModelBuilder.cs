using LivingLab.ML.Model;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;

namespace LivingLab.ML;
/// <summary>
/// Train, Evaluate and Create prediction model.
/// </summary>
public static class ModelBuilder
{
    const string DEVICE_TYPE = "DeviceType";
    const string DEVICE_SERIAL_NO = "DeviceSerialNo";
    const string ENERGY_USAGE = "EnergyUsage";
    const string INTERVAL = "Interval";
    const string LOGGED_AT = "LoggedAt";
    const string LOGGED_AT_TF = "LoggedAtTF";
    
    private static string TRAIN_DATA_PATH = Path.Combine(Environment.CurrentDirectory, "Data", "energy-usage.csv");
    private static string MODEL_FILE = ConsumeModel.MLNetModelPath;

    private static MLContext _mlContext = new MLContext(0);

    /// <summary>
    /// Call this method to train the model and save it to disk.
    /// </summary>
    public static void CreateModel()
    {
        // Load Data
        var data = _mlContext.Data.LoadFromTextFile<ModelInput>(TRAIN_DATA_PATH, hasHeader: true,
            separatorChar: ',');
        
        // Split data for training and testing
        var trainTestData = _mlContext.Data.TrainTestSplit(data, testFraction: 0.2, seed: 0);
        var trainData = trainTestData.TrainSet;
        var testData = trainTestData.TestSet;

        // Build training pipeline
        var trainingPipeline = BuildPipeline();

        // Train model
        var mlModel = TrainModel(trainData, trainingPipeline);
        
        // Evaluate quality of model
        Evaluate(testData, trainingPipeline);

        // Save model
        SaveModel(mlModel, trainData.Schema);
    }

    /// <summary>
    /// Builds the pipeline for the training.
    /// </summary>
    ///
    /// <returns>Training pipeline</returns>
    private static IEstimator<ITransformer> BuildPipeline()
    {
        // Data process configuration with pipeline data transformations 
        var dataProcessPipeline = _mlContext.Transforms.Categorical
            .OneHotEncoding(new[] { 
                new InputOutputColumnPair(DEVICE_TYPE, DEVICE_TYPE), 
                new InputOutputColumnPair(DEVICE_SERIAL_NO, DEVICE_SERIAL_NO) })
            .Append(_mlContext.Transforms.Text.FeaturizeText(LOGGED_AT_TF, LOGGED_AT))
            .Append(_mlContext.Transforms.Concatenate("Features", new[] { DEVICE_TYPE, DEVICE_SERIAL_NO, LOGGED_AT_TF,INTERVAL }));
        
        // Set the training algorithm 
        var trainer = _mlContext.Regression.Trainers.FastTreeTweedie(new FastTreeTweedieTrainer.Options
        {
            NumberOfLeaves = 2, 
            MinimumExampleCountPerLeaf = 10, 
            NumberOfTrees = 500, 
            LearningRate = 0.25817448f, 
            Shrinkage = 0.37814042f, 
            LabelColumnName = ENERGY_USAGE, 
            FeatureColumnName = "Features"
        });

        var trainingPipeline = dataProcessPipeline.Append(trainer);

        return trainingPipeline;
    }

    /// <summary>
    /// Trains the model using the pipeline created.
    /// </summary>
    /// 
    /// <param name="trainingDataView">Training data</param>
    /// <param name="trainingPipeline">Training pipeline</param>
    /// 
    /// <returns>Trained model</returns>
    private static ITransformer TrainModel(IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
    {
        Console.WriteLine("=============== Training  model ===============");

        ITransformer model = trainingPipeline.Fit(trainingDataView);

        Console.WriteLine("=============== End of training process ===============");
        return model;
    }

    /// <summary>
    /// Evaluate the quality of the model.
    /// </summary>
    /// 
    /// <param name="testDataView">Testing data</param>
    /// <param name="trainingPipeline">Training pipeline</param>
    private static void Evaluate(IDataView testDataView, IEstimator<ITransformer> trainingPipeline)
    {
        Console.WriteLine("=============== Cross-validating to get model's accuracy metrics ===============");
        var crossValidationResults = _mlContext.Regression
            .CrossValidate(testDataView, trainingPipeline, numberOfFolds: 5, labelColumnName:ENERGY_USAGE);
        PrintRegressionFoldsAverageMetrics(crossValidationResults);
    }

    /// <summary>
    /// Saves the model into a ZIP file.
    /// </summary>
    /// 
    /// <param name="mlModel">Trained model</param>
    /// <param name="modelInputSchema">Schema for trained data</param>
    private static void SaveModel(ITransformer mlModel, DataViewSchema modelInputSchema)
    {
        Console.WriteLine($"=============== Saving the model  ===============");
        _mlContext.Model.Save(mlModel, modelInputSchema, GetAbsolutePath(MODEL_FILE));
        Console.WriteLine("The model is saved to {0}", GetAbsolutePath(MODEL_FILE));
    }
    
    /// <summary>
    /// Retrieves the absolute path of a file.
    /// </summary>
    /// 
    /// <param name="relativePath">Relative path of file</param>
    /// <returns>Absolute path of file</returns>
    private static string GetAbsolutePath(string relativePath)
    {
        FileInfo dataRoot = new FileInfo(typeof(Program).Assembly.Location);
        string assemblyFolderPath = dataRoot.Directory.FullName;

        string fullPath = Path.Combine(assemblyFolderPath, relativePath);

        return fullPath;
    }
    
    public static void PrintRegressionMetrics(RegressionMetrics metrics)
    {
        Console.WriteLine($"*************************************************");
        Console.WriteLine($"*       Metrics for Regression model      ");
        Console.WriteLine($"*------------------------------------------------");
        Console.WriteLine($"*       LossFn:        {metrics.LossFunction:0.##}");
        Console.WriteLine($"*       R2 Score:      {metrics.RSquared:0.##}");
        Console.WriteLine($"*       Absolute loss: {metrics.MeanAbsoluteError:#.##}");
        Console.WriteLine($"*       Squared loss:  {metrics.MeanSquaredError:#.##}");
        Console.WriteLine($"*       RMS loss:      {metrics.RootMeanSquaredError:#.##}");
        Console.WriteLine($"*************************************************");
    }
    
    private static void PrintRegressionFoldsAverageMetrics(IEnumerable<TrainCatalogBase.CrossValidationResult<RegressionMetrics>> crossValidationResults)
    {
        var L1 = crossValidationResults.Select(r => r.Metrics.MeanAbsoluteError);
        var L2 = crossValidationResults.Select(r => r.Metrics.MeanSquaredError);
        var RMS = crossValidationResults.Select(r => r.Metrics.RootMeanSquaredError);
        var lossFunction = crossValidationResults.Select(r => r.Metrics.LossFunction);
        var R2 = crossValidationResults.Select(r => r.Metrics.RSquared);

        Console.WriteLine($"*************************************************************************************************************");
        Console.WriteLine($"*       Metrics for Regression model      ");
        Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"*       Average L1 Loss:       {L1.Average():0.###} ");
        Console.WriteLine($"*       Average L2 Loss:       {L2.Average():0.###}  ");
        Console.WriteLine($"*       Average RMS:           {RMS.Average():0.###}  ");
        Console.WriteLine($"*       Average Loss Function: {lossFunction.Average():0.###}  ");
        Console.WriteLine($"*       Average R-squared:     {R2.Average():0.###}  ");
        Console.WriteLine($"*************************************************************************************************************");
    }
}
