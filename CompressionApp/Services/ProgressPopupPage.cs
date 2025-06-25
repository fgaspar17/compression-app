namespace CompressionApp.Services;

public class ProgressPopupPage : ContentPage
{
    private readonly ProgressBar _progressBar;

    public ProgressPopupPage()
    {
        _progressBar = new ProgressBar
        {
            Progress = 0,
            HeightRequest = 20,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            Margin = new Thickness(20)
        };

        Content = new StackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            Children = {
                new Label { Text = "Processing...", HorizontalOptions = LayoutOptions.Center },
                _progressBar
            }
        };
    }

    public void UpdateProgress(double percent)
    {
        // percent should be between 0 and 1
        MainThread.BeginInvokeOnMainThread(() =>
        {
            _progressBar.Progress = percent;
        });
    }
}