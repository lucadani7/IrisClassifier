using Microsoft.ML.Data;

namespace IrisClassifierAvalonia;

public class IrisPrediction {
    [ColumnName("PredictedLabel")]
    public string? PredictedLabels;
}