using Microsoft.ML;
using Microsoft.ML.Data;

namespace IrisClassifierAvalonia;

public class IrisModel {
    private readonly PredictionEngine<IrisData, IrisPrediction> _predictionEngine;

    public MulticlassClassificationMetrics Metrics { get; private set; }

    public IrisModel(string dataPath)
    {
        var mlContext = new MLContext();

        var dataView = mlContext.Data.LoadFromTextFile<IrisData>(dataPath, hasHeader: false, separatorChar: ',');

        var split = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

        var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
            .Append(mlContext.Transforms.Concatenate("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"))
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        ITransformer model = pipeline.Fit(split.TrainSet);

        var predictions = model.Transform(split.TestSet);
        Metrics = mlContext.MulticlassClassification.Evaluate(predictions);

        _predictionEngine = mlContext.Model.CreatePredictionEngine<IrisData, IrisPrediction>(model);
    }

    public string Predict(float sepalLength, float sepalWidth, float petalLength, float petalWidth)
    {
        var input = new IrisData
        {
            SepalLength = sepalLength,
            SepalWidth = sepalWidth,
            PetalLength = petalLength,
            PetalWidth = petalWidth
        };

        var prediction = _predictionEngine.Predict(input);
        return prediction.PredictedLabels ?? string.Empty;
    }
}