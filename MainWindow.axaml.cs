using Avalonia.Controls;
using Avalonia.Interactivity;

namespace IrisClassifierAvalonia;

public partial class MainWindow : Window {
    private readonly IrisModel _irisModel;

    public MainWindow()
    {
        InitializeComponent();
        _irisModel = new IrisModel("/Users/lucadani/RiderProjects/IrisClassifierAvalonia/irisdata.txt");
        MacroAccuracyTextBlock.Text = $"MacroAccuracy: {_irisModel.Metrics.MacroAccuracy:F2}";
        MicroAccuracyTextBlock.Text = $"MicroAccuracy: {_irisModel.Metrics.MicroAccuracy:F2}";
        LogLossTextBlock.Text = $"LogLoss: {_irisModel.Metrics.LogLoss:F2}";
    }

    private void OnPredictClick(object? sender, RoutedEventArgs e) {
        if (float.TryParse(SepalLengthTextBox.Text, out var sepalLength) &&
            float.TryParse(SepalWidthTextBox.Text, out var sepalWidth) &&
            float.TryParse(PetalLengthTextBox.Text, out var petalLength) &&
            float.TryParse(PetalWidthTextBox.Text, out var petalWidth)) {
            var result = _irisModel.Predict(sepalLength, sepalWidth, petalLength, petalWidth);
            PredictionTextBox.Text = result;
            return;
        }
        PredictionTextBox.Text = "Invalid input.";
    }
    
    private void OnClearAllClick(object? sender, RoutedEventArgs e)
    {
        SepalLengthTextBox.Text = string.Empty;
        SepalWidthTextBox.Text = string.Empty;
        PetalLengthTextBox.Text = string.Empty;
        PetalWidthTextBox.Text = string.Empty;
        PredictionTextBox.Text = string.Empty;
    }
}

