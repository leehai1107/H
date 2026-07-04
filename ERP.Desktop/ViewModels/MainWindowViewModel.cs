namespace ERP.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private object? _currentView;

    public object? CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel()
    {
        CurrentView = new DashboardViewModel();
    }
}