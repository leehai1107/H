using CommunityToolkit.Mvvm.Input;

namespace ERP.Desktop.ViewModels;

public class ShellActionViewModel
{
    public ShellActionViewModel(string glyph, string toolTip, IRelayCommand command)
    {
        Glyph = glyph;
        ToolTip = toolTip;
        Command = command;
    }

    public string Glyph { get; }

    public string ToolTip { get; }

    public IRelayCommand Command { get; }
}