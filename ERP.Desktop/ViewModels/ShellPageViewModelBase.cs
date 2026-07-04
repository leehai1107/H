using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;

namespace ERP.Desktop.ViewModels;

public abstract class ShellPageViewModelBase : ViewModelBase
{
    private string _headerTitle = string.Empty;
    private string _statusText = string.Empty;
    private Brush _statusBrush = Brushes.Gray;

    public ObservableCollection<ShellActionViewModel> HeaderActions { get; } = new();

    public string HeaderTitle
    {
        get => _headerTitle;
        protected set
        {
            _headerTitle = value;
            OnPropertyChanged();
        }
    }

    public string StatusText
    {
        get => _statusText;
        protected set
        {
            _statusText = value;
            OnPropertyChanged();
        }
    }

    public Brush StatusBrush
    {
        get => _statusBrush;
        protected set
        {
            _statusBrush = value;
            OnPropertyChanged();
        }
    }

    protected void SetStatus(string statusText, Brush statusBrush)
    {
        StatusText = statusText;
        StatusBrush = statusBrush;
    }

    protected void SetHeaderTitle(string headerTitle)
    {
        HeaderTitle = headerTitle;
    }

    protected void AddHeaderAction(string glyph, string toolTip, IRelayCommand command)
    {
        HeaderActions.Add(new ShellActionViewModel(glyph, toolTip, command));
    }

    protected void ClearHeaderActions()
    {
        HeaderActions.Clear();
    }
}